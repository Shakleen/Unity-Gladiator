public class AIChaseState : AIBaseState
{
    public AIChaseState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override AIStateType GetStateType() { return AIStateType.chase; }

    public override void OnEnterState() {}

    public override void OnExitState() {}

    public override void CheckSwitchState() {}

    public override void ExecuteState() {}
}