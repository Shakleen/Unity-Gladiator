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
        _currentState = _states[GetIndex(AIStateEnum.aware)];
    }

    private void InitializeStates()
    {
        _states = new AIBaseState[System.Enum.GetNames(typeof(AIStateEnum)).Length];
        var assembly = typeof(AIBaseState).Assembly;

        foreach(var type in assembly.GetExportedTypes())
        {
            if (type.IsSubclassOf(typeof(AIBaseState)))
            {
                AIBaseState state = (AIBaseState) Activator.CreateInstance(type, _aiAgent, this);
                int index = GetIndex(CastStateEnum(state.GetStateType()));
                _states[index] = state;
            }
        }
    }

    public void ExecuteState() => _currentState.ExecuteState();

    public void SwitchState(Transition transition)
    {
        AIStateEnum newStateEnum = CastStateEnum(transition.destination);
        _currentState.OnExitState(transition);
        _currentState = _states[GetIndex(newStateEnum)];
        _currentState.OnEnterState(transition);
        _aiAgent.CurrentState = newStateEnum;
    }

    public AIStateEnum GetCurrentStateType() => CastStateEnum(_currentState.GetStateType());

    private AIStateEnum CastStateEnum(Enum state) => (AIStateEnum) state;

    private static int GetIndex(AIStateEnum stateType) => (int)(object)stateType;
}