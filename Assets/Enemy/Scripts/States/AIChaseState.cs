using UnityEngine;

public class AIChaseState : AIBaseState
{
    float _timer;
    
    public AIChaseState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override AIStateType GetStateType() { return AIStateType.chase; }

    public override void OnEnterState() { _timer = _aiAgent.Config.MinimumUpdateWaitTime; }

    public override void OnExitState() {}

    public override void CheckSwitchState() {
        if (!IsWithInReach()) 
            _stateMachine.SwitchState(AIStateType.idle);
    }

    private bool IsWithInReach() 
    { 
        Vector3 distanceFromPlayer = _aiAgent.PlayerTransform.position - _aiAgent.transform.position;
        return distanceFromPlayer.magnitude <= _aiAgent.Config.AwarenessRadius; 
    }

    public override void ExecuteState()
    {
        CheckSwitchState();
        _aiAgent.AILocomotion.UpdateAgentPath();
        _aiAgent.AnimationHandler.SetAnimationValueMovementSpeed(_aiAgent.AILocomotion.NavMeshAgent.velocity.magnitude);
    }
}