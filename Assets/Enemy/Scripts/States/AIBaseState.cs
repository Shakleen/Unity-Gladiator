public enum AIStateType { idle, chase, attack, explore, death }

public abstract class AIBaseState
{
    protected AIAgent _aiAgent;
    protected AIStateMachine _stateMachine;

    protected AIBaseState(AIAgent aiAgent, AIStateMachine stateMachine)
    {
        _aiAgent = aiAgent;
        _stateMachine = stateMachine;
    }

    public abstract AIStateType GetStateType();

    public abstract void OnEnterState();

    public abstract void OnExitState();

    public void CheckSwitchState()
    {
        if (_aiAgent.Health.IsEmpty())
            _stateMachine.SwitchState(AIStateType.death);
    }

    public abstract void ExecuteState();
}