using System;
using UnityEngine;

public class AIAwareState : AIBaseState
{
    public AIAwareState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override void InitializeTransitions()
    {
        _transtions.Add(new Transition(AIStateType.chase, ToChaseCondition));
        _transtions.Add(new Transition(AIStateType.attack, ToAttackCondition, OnExitToAttack));
        _transtions.Add(new Transition(AIStateType.taunt, ToTauntCondition, OnExitToTaunt));
    }

    #region Transition conditions
    public bool ToChaseCondition() => IsWithInAwarenessRadius() && !IsWithInAttackRadius();

    private bool ToAttackCondition()
    {
        bool willAttack = UnityEngine.Random.Range(0f, 1f) <= _aiAgent.Config.AttackChance;
        bool isCoolDownOver = _aiAgent.AILocomotion.HasTimerRunOut(_aiAgent.AILocomotion.AttackCoolDownTimer);
        return IsWithInAttackRadius() && willAttack && isCoolDownOver;
    }

    private bool ToTauntCondition()
    {
        bool reachedMaxTaunts = _aiAgent.AILocomotion.ReachedMaxTaunts();
        bool isCoolDownOver = _aiAgent.AILocomotion.HasTimerRunOut(_aiAgent.AILocomotion.TauntCoolDownTimer);
        return IsWithInAttackRadius() && !reachedMaxTaunts && isCoolDownOver;
    }

    private bool IsWithInAttackRadius()
    {
        float distanceFromPlayer = _aiAgent.AILocomotion.DistanceFromPlayerSqrMagnitude();
        return distanceFromPlayer <= Square(_aiAgent.Config.AttackRadius);
    }

    private bool IsWithInAwarenessRadius()
    {
        float distanceFromPlayer = _aiAgent.AILocomotion.DistanceFromPlayerSqrMagnitude();
        return distanceFromPlayer <= Square(_aiAgent.Config.AwarenessRadius);
    }

    private float Square(float value) => value * value;

    private float DistanceFromPlayer()=> (_aiAgent.PlayerTransform.position - _aiAgent.transform.position).magnitude;
    #endregion

    #region Transition On Exit functions
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

    public override Enum GetStateType() => AIStateType.aware;

    public override void OnEnterState() {}

    public override void ExecuteState() 
    { 
        CheckSwitchState(); 
        _aiAgent.AnimationHandler.SetAnimationValueMovementSpeed(0f);
    }
}