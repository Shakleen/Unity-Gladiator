using System;

public class AIDeathState : AIBaseState
{
    public AIDeathState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override Enum GetStateType() => AIStateEnum.death;

    public override void OnEnterState(Transition transition) {}

    public override void ExecuteState() {}

    public override void OnExitState(Transition transition) {}

    public override Transition GetTransition() => null;
}