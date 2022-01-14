public class AIExploreState : AIBaseState
{
    public AIExploreState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override AIStateType GetStateType() { return AIStateType.explore; }

    public override void OnEnterState() {}

    public override void OnExitState() {}

    public new void CheckSwitchState() { base.CheckSwitchState(); }

    public override void ExecuteState() {}
}