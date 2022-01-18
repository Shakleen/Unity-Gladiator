using System;

public class AITauntState : AIBaseState
{
    private bool _isAnimationPlaying, _isTaunting;

    public AITauntState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override void InitializeTransitions() => _transtions.Add(new Transition(AIStateType.aware, ToAwareCondition, OnExitState));
    
    private bool ToAwareCondition() => !_aiAgent.AnimationHandler.IsTaunting;

    public override Enum GetStateType() => AIStateType.taunt;

    public override void OnEnterState() {}

    private void OnExitState() => _aiAgent.AnimationHandler.SetAnimationValueIsTaunting(false);

    public override void ExecuteState() => CheckSwitchState();

    private bool IsWithInAttackRadius() => GetDistanceFromPlayer() <= _aiAgent.Config.AttackRadius;

    private float GetDistanceFromPlayer() => (_aiAgent.PlayerTransform.position - _aiAgent.transform.position).magnitude;
}