using System;

public class AIIdleState : AIBaseState
{
    private bool _reachedMaxTaunts, _isCoolDownOver, _willAttack;
    private Transition _toChase, _toDeath, _toTaunt, _toAttack;

    public AIIdleState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine)
    {
        _toChase = new Transition(GetStateType(), AIStateEnum.chase);
        _toDeath = new Transition(GetStateType(), AIStateEnum.death);
        _toTaunt = new Transition(GetStateType(), AIStateEnum.taunt);
        _toAttack = new Transition(GetStateType(), AIStateEnum.attack);
    }

    public override Enum GetStateType() => AIStateEnum.idle;

    public override void OnEnterState(Transition transition) {}

    public override void ExecuteState() => CheckSwitchState();

    #region Switch state conditions
    public override Transition GetTransition()
    {
        if (IsDead())
            return _toDeath;
        else if (IsInReach())
        {
            // if (ToAttackCondition())
                return _toAttack;
            // else if (ToTauntCondition())
            //     return _toTaunt;
        }
        else if (IsInAwareness() && !IsInReach())
            return _toChase;

        return null;
    }

    private bool ToAttackCondition()
    {
        _willAttack = UnityEngine.Random.Range(0f, 1f) <= _aiAgent.Config.AttackChance;
        _isCoolDownOver = _aiAgent.locomotion.HasTimerRunOut(_aiAgent.locomotion.AttackCoolDownTimer);
        return _willAttack && _isCoolDownOver;
    }

    private bool ToTauntCondition()
    {
        _reachedMaxTaunts = _aiAgent.locomotion.ReachedMaxTaunts();
        _isCoolDownOver = _aiAgent.locomotion.HasTimerRunOut(_aiAgent.locomotion.TauntCoolDownTimer);
        return !_reachedMaxTaunts && _isCoolDownOver;
    }
    #endregion

    #region Transition On Exit functions
    public override void OnExitState(Transition transition)
    {
        if (transition.destination.CompareTo(AIStateEnum.attack) == 0)
            OnExitToAttack();
        else if (transition.destination.CompareTo(AIStateEnum.taunt) == 0)
            OnExitToTaunt();
    }

    private void OnExitToAttack() 
    { 
        _aiAgent.locomotion.ResetAttackCoolDown();
        _aiAgent.locomotion.ResetTauntCount();
        _aiAgent.AnimationHandler.SetAnimationValueIsAttacking(true); 
    }

    private void OnExitToTaunt() 
    { 
        _aiAgent.locomotion.ResetTauntCoolDown();
        _aiAgent.locomotion.IncremetTauntCount();
        _aiAgent.AnimationHandler.SetAnimationValueIsTaunting(true); 
    }
    #endregion
}