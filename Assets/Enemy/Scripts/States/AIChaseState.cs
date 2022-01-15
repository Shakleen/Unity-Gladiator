using UnityEngine;

public class AIChaseState : AIBaseState
{
    float _timer;
    
    public AIChaseState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override AIStateType GetStateType() { return AIStateType.chase; }

    public override void OnEnterState() { _timer = _aiAgent.Config.MinimumUpdateWaitTime; }

    public override void OnExitState() {}

    public override void CheckSwitchState() {}

    public override void ExecuteState()
    {
        CheckSwitchState();
        UpdateAgentPath();
        _aiAgent.AnimationHandler.SetAnimationValueMovementSpeed(_aiAgent.NavMeshAgent.velocity.magnitude);
    }

    private void UpdateAgentPath()
    {
        _timer -= Time.deltaTime;

        if (_timer < 0f)
        {
            float thresh = _aiAgent.Config.MinimumUpdateDistance * _aiAgent.Config.MinimumUpdateDistance;
            float playerMoveDistance = (_aiAgent.PlayerTransform.position - _aiAgent.NavMeshAgent.destination).sqrMagnitude;

            if (playerMoveDistance > thresh)
                _aiAgent.NavMeshAgent.destination = _aiAgent.PlayerTransform.position;

            _timer = _aiAgent.Config.MinimumUpdateWaitTime;
        }
    }
}