using System;

public class AIAttackState : AIBaseState
{
    public AIAttackState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) {}

    public override void InitializeTransitions()
    {
        _transtions.Add(new Transition(AIStateType.aware, ToAwareCondition));
    }

    private bool ToAwareCondition()
    {
        throw new NotImplementedException();
    }

    public override Enum GetStateType() => AIStateType.attack;

    public override void OnEnterState() {}

    public override void ExecuteState() => CheckSwitchState();
}