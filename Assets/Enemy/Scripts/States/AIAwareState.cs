using System;
using UnityEngine;

public class AIAwareState : AIBaseState
{
    private const float TIMER_THRESH = 1e-5f;
    private float _attackTimer, _tauntTimer;
    private float _distanceFromPlayer, _attackCoolDown;
    private bool _isWithInAwareness, _isWithInAttackRadius, _willAttack, _attackCoolDownOver;

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
        return _isWithInAttackRadius && HasTimerRunOut(_tauntTimer);
    }

    private float DistanceFromPlayer()=> (_aiAgent.PlayerTransform.position - _aiAgent.transform.position).magnitude;
    #endregion

    private bool HasTimerRunOut(float timer) => timer <= TIMER_THRESH;

    #region Transition On Exit functions
    private void OnExitToAttack() => _attackTimer = _aiAgent.Config.AttackCoolDown;

    private void OnExitToTaunt() => _tauntTimer = _aiAgent.Config.TauntCoolDown;
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

    private float TimerCountDown(float tiemr)
    {
        if (tiemr > TIMER_THRESH)
            tiemr -= Time.deltaTime;
        else
            tiemr = 0f;
        
        return tiemr;
    }
}