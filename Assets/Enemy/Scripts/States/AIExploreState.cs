public class AIExploreState : AIBaseState
{
    public AIExploreState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override AIStateType GetStateType() { return AIStateType.explore; }

    public override void OnEnterState() {}

    public override void OnExitState() {}

    public override void CheckSwitchState() {}

    public override void ExecuteState() {}
}