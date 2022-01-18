using System;
using UnityEngine;

public class AIChaseState : AIBaseState
{
    float _timer;
    
    public AIChaseState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override void InitializeTransitions() => _transtions.Add(new Transition(AIStateType.aware, ToAwareCondition));

    private bool ToAwareCondition()
    {
        Vector3 distanceFromPlayer = _aiAgent.PlayerTransform.position - _aiAgent.transform.position;
        bool isWithInAttackDistance = distanceFromPlayer.magnitude <= _aiAgent.Config.AttackRadius;
        return isWithInAttackDistance;
    }

    public override Enum GetStateType() => AIStateType.chase;

    public override void OnEnterState() {}

    public override void ExecuteState()
    {
        CheckSwitchState();
        _aiAgent.AILocomotion.UpdateAgentPath();
        _aiAgent.AnimationHandler.SetAnimationValueMovementSpeed(_aiAgent.AILocomotion.NavMeshAgent.velocity.magnitude);
    }
}