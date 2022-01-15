public class AIDeathState : AIBaseState
{
    public AIDeathState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override AIStateType GetStateType() { return AIStateType.death; }

    public override void OnEnterState() { _aiAgent.AnimationHandler.SetAnimationValueIsDead(true); }

    public override void OnExitState() {}

    public override void CheckSwitchState() {}

    public override void ExecuteState() {
        if (!_aiAgent.AnimationHandler.IsAnimationPlaying() && !_aiAgent.RagDollHandler.IsRagDollActive)
            _aiAgent.RagDollHandler.SetRagDollStatus(true);
    }
}