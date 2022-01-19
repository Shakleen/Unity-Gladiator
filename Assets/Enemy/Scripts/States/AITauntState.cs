using System;

public class AITauntState : AIBaseState
{
    private Transition _toAware, _toDeath;

    public AITauntState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override Enum GetStateType() => AIStateEnum.taunt;

    public override void OnEnterState(Transition transition) {}

    private bool IsWithInAttackRadius() => GetDistanceFromPlayer() <= _aiAgent.Config.AttackRadius;

    private float GetDistanceFromPlayer() => (_aiAgent.PlayerTransform.position - _aiAgent.transform.position).magnitude;

    public override void ExecuteState() => CheckSwitchState();

    public override Transition GetTransition()
    {
        if (_aiAgent.Health.IsEmpty())
            return _toDeath;
        else if (!_aiAgent.AnimationHandler.IsAttacking)
            return _toAware;
        
        return null;
    }

    public override void OnExitState(Transition transition) => _aiAgent.AnimationHandler.SetAnimationValueIsTaunting(false);
}