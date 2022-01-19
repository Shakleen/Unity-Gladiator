public enum AIStateEnum { aware, chase, attack, taunt, death }

public abstract class AIBaseState : BaseStateClass
{
    protected AIAgent _aiAgent;
    protected AIStateMachine _stateMachine;

    protected AIBaseState(AIAgent aiAgent, AIStateMachine stateMachine)
    {
        _aiAgent = aiAgent;
        _stateMachine = stateMachine;
    }

    public override void CheckSwitchState()
    {
        Transition _transition = GetTransition();
        if (_transition != null)
            _stateMachine.SwitchState(_transition);
    }
}