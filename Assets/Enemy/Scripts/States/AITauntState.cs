using System;
using UnityEngine;

public class AITauntState : AIBaseState
{
    public AITauntState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override void InitializeTransitions() => _transtions.Add(new Transition(AIStateType.aware, ToAwareCondition));
    
    private bool ToAwareCondition()
    {
        Vector3 distanceFromPlayer = _aiAgent.PlayerTransform.position - _aiAgent.transform.position;
        bool isWithInAttackDistance = distanceFromPlayer.magnitude <= _aiAgent.Config.AttackRadius;
        return !isWithInAttackDistance;
    }

    public override Enum GetStateType() => AIStateType.taunt;

    public override void OnEnterState() {}

    public override void ExecuteState() => CheckSwitchState();

    private bool IsWithInAttackRadius() => GetDistanceFromPlayer() <= _aiAgent.Config.AttackRadius;

    private float GetDistanceFromPlayer() => (_aiAgent.PlayerTransform.position - _aiAgent.transform.position).magnitude;
}