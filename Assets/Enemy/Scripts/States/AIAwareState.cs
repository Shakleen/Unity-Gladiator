using System;
using UnityEngine;

public class AIAwareState : AIBaseState
{
    private const float TIMER_THRESH = 1e-3f;
    private float _attackTimer, _tauntTimer;
    private float _distanceFromPlayer, _attackCoolDown;
    private bool _isWithInAwareness, _isWithInAttackRadius, _willAttack, _attackCoolDownOver, _reachedMaxTaunts;
    private int _tauntCount;

    public AIAwareState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override void InitializeTransitions()
    {
        _transtions.Add(new Transition(AIStateType.chase, ToChaseCondition));
        _transtions.Add(new Transition(AIStateType.attack, ToAttackCondition, OnExitToAttack));
        _transtions.Add(new Transition(AIStateType.taunt, ToTauntCondition, OnExitToTaunt));
    }

    #region Transition conditions
    public bool ToChaseCondition()
    {
        _distanceFromPlayer = DistanceFromPlayer();
        _isWithInAwareness = _distanceFromPlayer <= _aiAgent.Config.AwarenessRadius;
        _isWithInAttackRadius = _distanceFromPlayer <= _aiAgent.Config.AttackRadius;
        return _isWithInAwareness && !_isWithInAttackRadius;
    }

    private bool ToAttackCondition()
    {
        _willAttack = UnityEngine.Random.Range(0f, 1f) <= _aiAgent.Config.AttackChance;
        _isWithInAttackRadius = DistanceFromPlayer() <= _aiAgent.Config.AttackRadius;
        return _willAttack && _isWithInAttackRadius & HasTimerRunOut(_attackTimer);
    }

    private bool ToTauntCondition()
    {
        _isWithInAttackRadius = DistanceFromPlayer() <= _aiAgent.Config.AttackRadius;
        _reachedMaxTaunts = _tauntCount == _aiAgent.Config.MaxTaunts;
        return _isWithInAttackRadius && !_reachedMaxTaunts && HasTimerRunOut(_tauntTimer);
    }

    private float DistanceFromPlayer()=> (_aiAgent.PlayerTransform.position - _aiAgent.transform.position).magnitude;
    #endregion

    private bool HasTimerRunOut(float timer) => timer <= TIMER_THRESH;

    #region Transition On Exit functions
    private void OnExitToAttack() 
    { 
        _attackTimer = _aiAgent.Config.AttackCoolDown; 
        _tauntCount = 0;
        _aiAgent.AnimationHandler.SetAnimationValueIsAttacking(true); 
    }

    private void OnExitToTaunt() 
    { 
        _tauntTimer = _aiAgent.Config.TauntCoolDown;
        _tauntCount++;
        _aiAgent.AnimationHandler.SetAnimationValueIsTaunting(true); 
    }
    #endregion

    public override Enum GetStateType() => AIStateType.aware;

    public override void OnEnterState() {}

    public override void ExecuteState() 
    { 
        CheckSwitchState(); 
        _aiAgent.AnimationHandler.SetAnimationValueMovementSpeed(0f);
        _attackTimer = TimerCountDown(_attackTimer);
        _tauntTimer = TimerCountDown(_tauntTimer);
    }

    private float TimerCountDown(float timer)
    {
        if (HasTimerRunOut(timer))
            timer = 0f;
        else
            timer -= Time.deltaTime;
        
        return timer;
    }
}