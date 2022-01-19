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
        _currentState = _states[GetIndex(PlayerStateEnum.idle)];
    }

    private void InitializeStateArray()
    {
        _states = new PlayerBaseState[System.Enum.GetNames(typeof(PlayerStateEnum)).Length];
        var assembly = typeof(PlayerBaseState).Assembly;

        foreach(var type in assembly.GetExportedTypes())
        {
            if (type.IsSubclassOf(typeof(PlayerBaseState)) && !type.IsAbstract)
            {
                PlayerBaseState state = (PlayerBaseState) Activator.CreateInstance(type, _player, this);
                int index = GetIndex(CastStateEnum(state.GetStateType()));
                _states[index] = state;
            }
        }
    }

    public void SwitchState(Transition transition)
    {
        PlayerStateEnum newStateEnum = CastStateEnum(transition.destination);
        _currentState.OnExitState(transition);
        _currentState = _states[GetIndex(newStateEnum)];
        _currentState.OnEnterState(transition);
        _player.CurrentState = newStateEnum;
    }

    public void ExecuteState() => _currentState.ExecuteState();

    public PlayerStateEnum GetCurrentStateType() => CastStateEnum(_currentState.GetStateType());

    private PlayerStateEnum CastStateEnum(Enum state) => (PlayerStateEnum) state;

    private static int GetIndex(PlayerStateEnum stateType) => (int)(object)stateType;
}