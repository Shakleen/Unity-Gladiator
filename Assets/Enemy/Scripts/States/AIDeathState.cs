public class AIDeathState : AIBaseState
{
    public AIDeathState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override AIStateType GetStateType() { return AIStateType.death; }

    public override void OnEnterState() { _aiAgent.AnimationHandler.SetAnimationValueIsDead(true); }

    public override void OnExitState() {}

    public new void CheckSwitchState() {}

    public override void ExecuteState() {}
}