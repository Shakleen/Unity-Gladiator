using System;

public class AIAwareState : AIBaseState
{
    private bool _reachedMaxTaunts, _isCoolDownOver, _willAttack;
    private Transition _toChase, _toDeath, _toTaunt, _toAttack;

    public AIAwareState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine)
    {
        _toChase = new Transition(GetStateType(), AIStateEnum.chase);
        _toDeath = new Transition(GetStateType(), AIStateEnum.death);
        _toTaunt = new Transition(GetStateType(), AIStateEnum.taunt);
        _toAttack = new Transition(GetStateType(), AIStateEnum.attack);
    }

    public override Enum GetStateType() => AIStateEnum.aware;

    public override void OnEnterState(Transition transition) {}

    public override void ExecuteState() 
    { 
        CheckSwitchState(); 
        _aiAgent.AnimationHandler.SetAnimationValueMovementSpeed(0f);
    }

    #region Switch state conditions

    public override Transition GetTransition()
    {
        if (_aiAgent.Health.IsEmpty())
            return _toDeath;
        else if (IsWithInRadius(_aiAgent.Config.AttackChance))
        {
            if (ToAttackCondition())
                return _toAttack;
            else if (ToTauntCondition())
                return _toTaunt;
        }
        else if (IsWithInRadius(_aiAgent.Config.AwarenessRadius))
            return _toChase;

        return null;
    }

    private bool ToTauntCondition()
    {
        _reachedMaxTaunts = _aiAgent.AILocomotion.ReachedMaxTaunts();
        _isCoolDownOver = _aiAgent.AILocomotion.HasTimerRunOut(_aiAgent.AILocomotion.TauntCoolDownTimer);
        return !_reachedMaxTaunts && _isCoolDownOver;
    }

    private bool ToAttackCondition()
    {
        _willAttack = UnityEngine.Random.Range(0f, 1f) <= _aiAgent.Config.AttackChance;
        _isCoolDownOver = _aiAgent.AILocomotion.HasTimerRunOut(_aiAgent.AILocomotion.AttackCoolDownTimer);
        return _willAttack && _isCoolDownOver;
    }

    private bool IsWithInRadius(float radius) => _aiAgent.AILocomotion.DistanceFromPlayerSqrMagnitude() <= Square(radius);

    private float Square(float value) => value * value;
    #endregion


    #region Transition On Exit functions
    public override void OnExitState(Transition transition)
    {
        switch(transition.destination)
        {
            case AIStateEnum.attack:
                OnExitToAttack();
                break;
            case AIStateEnum.taunt:
                OnExitToTaunt();
                break;
        }
    }

    private void OnExitToAttack() 
    { 
        _aiAgent.AILocomotion.ResetAttackCoolDown();
        _aiAgent.AILocomotion.ResetTauntCount();
        _aiAgent.AnimationHandler.SetAnimationValueIsAttacking(true); 
    }

    private void OnExitToTaunt() 
    { 
        _aiAgent.AILocomotion.ResetTauntCoolDown();
        _aiAgent.AILocomotion.IncremetTauntCount();
        _aiAgent.AnimationHandler.SetAnimationValueIsTaunting(true); 
    }
    #endregion
}