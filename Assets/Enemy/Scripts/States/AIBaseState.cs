public enum AIStateType { aware, chase, attack, taunt, death }

public abstract class AIBaseState : BaseState
{
    protected AIAgent _aiAgent;
    protected AIStateMachine _stateMachine;

    protected AIBaseState(AIAgent aiAgent, AIStateMachine stateMachine)
    {
        _aiAgent = aiAgent;
        _stateMachine = stateMachine;
    }

    public void CheckSwitchState()
    {
        foreach(Transition transition in _transtions)
        {
            if (transition.Condition())
            {
                if (transition.OnExit != null) transition.OnExit();
                _stateMachine.SwitchState((AIStateType) transition.destination);
                return;
            }
        }
    }
}