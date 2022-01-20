using System;

public class AIChaseState : AIBaseState
{
    private Transition _toIdle, _toDeath;
    
    public AIChaseState(AIAgent aiAgent, AIStateMachine stateMachine) : base(aiAgent, stateMachine) 
    {
        _toIdle = new Transition(GetStateType(), AIStateEnum.idle);
        _toDeath = new Transition(GetStateType(), AIStateEnum.death);
    }

    public override Enum GetStateType() => AIStateEnum.chase;

    public override void OnEnterState(Transition transition) {}

    public override void ExecuteState()
    {
        CheckSwitchState();
        _aiAgent.locomotion.UpdateAgentPath();
    }

    public override Transition GetTransition()
    {
        if (IsDead())
            return _toDeath;
        else if (IsInReach())
            return _toIdle;

        return null;
    }

    public override void OnExitState(Transition transition) {}
}