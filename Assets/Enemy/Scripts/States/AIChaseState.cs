using System;
using UnityEngine;

public class AIChaseState : AIBaseState
{
    private Transition _toAware, _toDeath;
    
    public AIChaseState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) 
    {
        _toAware = new Transition(GetStateType(), AIStateEnum.aware);
        _toDeath = new Transition(GetStateType(), AIStateEnum.death);
    }

    public override Enum GetStateType() => AIStateEnum.chase;

    public override void OnEnterState(Transition transition) {}

    public override void ExecuteState()
    {
        CheckSwitchState();
        _aiAgent.AILocomotion.UpdateAgentPath();
        _aiAgent.AnimationHandler.SetAnimationValueMovementSpeed(_aiAgent.AILocomotion.NavMeshAgent.velocity.magnitude);
    }

    public override Transition GetTransition()
    {
        if (_aiAgent.Health.IsEmpty())
            return _toDeath;
        else if (!ToAwareCondition())
            return _toAware;

        return null;
    }

    private bool ToAwareCondition()
    {
        Vector3 distanceFromPlayer = _aiAgent.PlayerTransform.position - _aiAgent.transform.position;
        bool isWithInAttackDistance = distanceFromPlayer.magnitude <= _aiAgent.Config.AttackRadius;
        return isWithInAttackDistance;
    }

    public override void OnExitState(Transition transition) {}
}