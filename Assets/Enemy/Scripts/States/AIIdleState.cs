public class AIIdleState : AIBaseState
{
    public AIIdleState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override AIStateType GetStateType() { return AIStateType.idle; }

    public override void OnEnterState() {}

    public override void OnExitState() {}

    public override void CheckSwitchState() {}

    public override void ExecuteState() {}
}