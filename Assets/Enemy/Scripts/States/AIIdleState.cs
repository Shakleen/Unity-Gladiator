using UnityEngine;

public class AIIdleState : AIBaseState
{
    public AIIdleState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override AIStateType GetStateType() { return AIStateType.idle; }

    public override void OnEnterState() {}

    public override void OnExitState() {}

    public override void ExecuteState() 
    { 
        CheckSwitchState(); 
        _aiAgent.AnimationHandler.SetAnimationValueMovementSpeed(0f);
    }

    public override void CheckSwitchState() 
    { 
        if (ShouldChasePlayer())
            _stateMachine.SwitchState(AIStateType.chase);
    }

    public bool ShouldChasePlayer()
    {
        Vector3 distanceFromPlayer = _aiAgent.PlayerTransform.position - _aiAgent.transform.position;
        return IsWithInReach(distanceFromPlayer) && IsFacingPlayer(distanceFromPlayer);
    }

    private bool IsWithInReach(Vector3 distanceFromPlayer) { return distanceFromPlayer.magnitude <= _aiAgent.Config.AwarenessRadius; }

    private bool IsFacingPlayer(Vector3 distanceFromPlayer)
    {
        distanceFromPlayer.Normalize();
        float dotProduct = Vector3.Dot(distanceFromPlayer, _aiAgent.transform.forward);
        return dotProduct > 0.0f;
    }
}