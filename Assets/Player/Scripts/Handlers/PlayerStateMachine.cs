using System;

public class PlayerStateMachine
{
    private Player _player;
    private PlayerBaseState[] _states;
    private PlayerBaseState _currentState;

    public PlayerStateMachine(Player context)
    {
        _player = context;
        InitializeStateArray();
        _currentState = _states[GetIndex(PlayerStateType.idle)];
    }

    private void InitializeStateArray()
    {
        _states = new PlayerBaseState[System.Enum.GetNames(typeof(PlayerStateType)).Length];
        var assembly = typeof(PlayerBaseState).Assembly;

        foreach(var type in assembly.GetExportedTypes())
        {
            if (type.IsSubclassOf(typeof(PlayerBaseState)) && !type.IsAbstract)
            {
                PlayerBaseState state = (PlayerBaseState) Activator.CreateInstance(type, _player, this);
                int index = GetIndex(GetStateType(state.GetStateType()));
                _states[index] = state;
            }
        }
    }

    public void SwitchState(PlayerStateType newStateType)
    {
        PlayerBaseState newState = CaseStateType(newStateType);
        newState.OnEnterState();
        _currentState = newState;
        _player.CurrentState = newStateType;
    }

    public void ExecuteState() => _currentState.ExecuteState();

    public PlayerStateType GetCurrentStateType() => GetStateType(_currentState.GetStateType());

    private PlayerStateType GetStateType(Enum state) => (PlayerStateType) state;

    private PlayerBaseState CaseStateType(PlayerStateType stateType) => _states[GetIndex(stateType)];

    private static int GetIndex(PlayerStateType stateType) => (int)(object)stateType;
}