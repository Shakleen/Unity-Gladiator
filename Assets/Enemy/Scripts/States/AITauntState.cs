using System;

public class AITauntState : AIBaseState
{
    private bool _isAnimationPlaying, _isTaunting;

    public AITauntState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override void InitializeTransitions() => _transtions.Add(new Transition(AIStateType.aware, ToAwareCondition, OnExitState));
    
    private bool ToAwareCondition()
    {
        _isAnimationPlaying = _aiAgent.AnimationHandler.IsAnimationPlaying();
        _isTaunting = _aiAgent.AnimationHandler.IsTaunting;
        return !_isAnimationPlaying && !_isTaunting;
    }

    public override Enum GetStateType() => AIStateType.taunt;

    public override void OnEnterState() {}

    private void OnExitState() => _aiAgent.AnimationHandler.SetAnimationValueIsTaunting(false);

    public override void ExecuteState() => CheckSwitchState();

    private bool IsWithInAttackRadius() => GetDistanceFromPlayer() <= _aiAgent.Config.AttackRadius;

    private float GetDistanceFromPlayer() => (_aiAgent.PlayerTransform.position - _aiAgent.transform.position).magnitude;
}