public class AIIdleState : AIBaseState
{
    public AIIdleState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override AIStateType GetStateType() { return AIStateType.idle; }

    public override void OnEnterState() {}

    public override void OnExitState() {}

    public new void CheckSwitchState() { base.CheckSwitchState(); }

    public override void ExecuteState() { CheckSwitchState(); }
}