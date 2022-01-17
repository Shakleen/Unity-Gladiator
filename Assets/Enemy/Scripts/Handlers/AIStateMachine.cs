using System;

public class AIStateMachine 
{
    private AIAgent _aiAgent;
    private AIBaseState[] _states;
    private AIBaseState _currentState;

    public AIStateMachine(AIAgent aiAgent)
    {
        _aiAgent = aiAgent;
        InitializeStates();
        _currentState = _states[GetIndex(AIStateType.idle)];
    }

    private void InitializeStates()
    {
        _states = new AIBaseState[System.Enum.GetNames(typeof(AIStateType)).Length];
        var assembly = typeof(AIBaseState).Assembly;

        foreach(var type in assembly.GetExportedTypes())
        {
            if (type.IsSubclassOf(typeof(AIBaseState)))
            {
                AIBaseState state = (AIBaseState) Activator.CreateInstance(type, _aiAgent, this);
                int index = GetIndex(state.GetStateType());
                _states[index] = state;
            }
        }
    }

    public void ExecuteState() 
    { 
        if (_currentState.GetStateType() != AIStateType.death)
        {
            if (_aiAgent.Health.IsEmpty())
                SwitchState(AIStateType.death);
            else
                _currentState.ExecuteState();
        }
    }

    public void SwitchState(AIStateType newStateType)
    {
        AIBaseState newState = _states[GetIndex(newStateType)];
        _currentState.OnExitState();
        newState.OnEnterState();
        _currentState = newState;
        _aiAgent.CurrentState = newStateType;
    }

    public AIStateType GetCurrentStateType() { return _currentState.GetStateType(); }

    private static int GetIndex(AIStateType stateType) { return (int)(object)stateType; }
}