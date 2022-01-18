using System;

public class AIDeathState : AIBaseState
{
    public AIDeathState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override void InitializeTransitions() {}

    public override Enum GetStateType() => AIStateType.death;

    public override void OnEnterState() => _aiAgent.AnimationHandler.SetAnimationValueIsDead(true);

    public override void ExecuteState() {
        if (!_aiAgent.AnimationHandler.IsAnimationPlaying() && !_aiAgent.RagDollHandler.IsRagDollActive)
            _aiAgent.RagDollHandler.SetRagDollStatus(true);
    }
}