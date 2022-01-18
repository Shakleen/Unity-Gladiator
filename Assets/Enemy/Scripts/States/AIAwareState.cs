using System;
using UnityEngine;

public class AIAwareState : AIBaseState
{
    public AIAwareState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override void InitializeTransitions()
    {
        _transtions.Add(new Transition(AIStateType.chase, ShouldChasePlayer));
    }

    public bool ShouldChasePlayer()
    {
        Vector3 distanceFromPlayer = _aiAgent.PlayerTransform.position - _aiAgent.transform.position;
        return distanceFromPlayer.magnitude <= _aiAgent.Config.AwarenessRadius;
    }

    public override Enum GetStateType() => AIStateType.aware;

    public override void OnEnterState() {}

    public override void ExecuteState() 
    { 
        CheckSwitchState(); 
        _aiAgent.AnimationHandler.SetAnimationValueMovementSpeed(0f);
    }
}