public class AIAttackState : AIBaseState
{
    public AIAttackState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override AIStateType GetStateType() { return AIStateType.attack; }

    public override void OnEnterState() {}

    public override void OnExitState() {}

    public override void CheckSwitchState() {}

    public override void ExecuteState() {}
}