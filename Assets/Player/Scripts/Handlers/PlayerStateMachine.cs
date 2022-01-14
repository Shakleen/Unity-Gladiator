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
            if (type.IsSubclassOf(typeof(PlayerBaseState)))
            {
                PlayerBaseState state = (PlayerBaseState) Activator.CreateInstance(type, _player, this);
                int index = GetIndex(state.GetStateType());
                _states[index] = state;
            }
        }
    }

    public void SwitchState(PlayerStateType newStateType)
    {
        PlayerBaseState newState = GetPlayerState(newStateType);
        _currentState.OnExitState();
        newState.OnEnterState();
        _currentState = newState;
    }

    public void ExecuteState() { _currentState.ExecuteState(); }

    public PlayerStateType GetCurrentStateType() { return _currentState.GetStateType(); }

    private PlayerBaseState GetPlayerState(PlayerStateType stateType) { return _states[GetIndex(stateType)]; }

    private static int GetIndex(PlayerStateType stateType) { return (int)(object)stateType; }
}