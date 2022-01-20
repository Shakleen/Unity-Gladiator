using System;

public class AITauntState : AIBaseState
{
    private Transition _toIdle, _toDeath;

    public AITauntState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine)
    {
        _toIdle = new Transition(GetStateType(), AIStateEnum.idle);
        _toDeath = new Transition(GetStateType(), AIStateEnum.death);
    }

    public override Enum GetStateType() => AIStateEnum.taunt;

    public override void OnEnterState(Transition transition) {}

    private float GetDistanceFromPlayer() => (_aiAgent.PlayerTransform.position - _aiAgent.transform.position).magnitude;

    public override void ExecuteState() => CheckSwitchState();

    public override Transition GetTransition()
    {
        if (IsDead())
            return _toDeath;
        else if (!_aiAgent.AnimationHandler.IsTaunting)
            return _toIdle;
        
        return null;
    }

    public override void OnExitState(Transition transition) => _aiAgent.AnimationHandler.SetAnimationValueIsTaunting(false);
}