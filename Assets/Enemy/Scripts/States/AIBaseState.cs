public enum AIStateType { idle, chase, attack, explore }

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

    public abstract void CheckSwitchState();

    public abstract void ExecuteState();
}