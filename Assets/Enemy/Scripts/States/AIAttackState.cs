using System;

public class AIAttackState : AIBaseState
{
    private bool _isAnimationPlaying, _isAttacking;

    public AIAttackState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override void InitializeTransitions() => _transtions.Add(new Transition(AIStateType.aware, ToAwareCondition, OnExitState));

    private bool ToAwareCondition() 
    {
        _isAnimationPlaying = _aiAgent.AnimationHandler.IsAnimationPlaying();
        _isAttacking = _aiAgent.AnimationHandler.IsAttacking;
        return !_isAnimationPlaying && !_isAttacking;
    }

    public override Enum GetStateType() => AIStateType.attack;

    public override void OnEnterState() {}

    public void OnExitState() => _aiAgent.AnimationHandler.SetAnimationValueIsAttacking(false);

    public override void ExecuteState() => CheckSwitchState();
}