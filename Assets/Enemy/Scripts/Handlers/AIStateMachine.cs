public class AIStateMachine 
{
    private AIAgent _aiAgent;
    private AIBaseState[] _states;
    private AIBaseState _currentState;

    public AIStateMachine(AIAgent aiAgent)
    {
        _aiAgent = aiAgent;
        InitializeStates();
    }

    private void InitializeStates()
    {
        _states = new AIBaseState[System.Enum.GetNames(typeof(AIStateType)).Length];
        _states[GetIndex(AIStateType.idle)] = new AIIdleState(_aiAgent, this);
        _states[GetIndex(AIStateType.explore)] = new AIExploreState(_aiAgent, this);
        _states[GetIndex(AIStateType.chase)] = new AIChaseState(_aiAgent, this);
        _states[GetIndex(AIStateType.attack)] = new AIAttackState(_aiAgent, this);
    }

    public void ExecuteState() { _currentState.ExecuteState(); }

    public void SwitchState(AIStateType newStateType)
    {
        AIBaseState newState = _states[GetIndex(newStateType)];
        _currentState.OnExitState();
        newState.OnEnterState();
        _currentState = newState;
    }

    public AIStateType GetCurrentStateType() { return _currentState.GetStateType(); }

    private static int GetIndex(AIStateType stateType) { return (int)(object)stateType; }
}