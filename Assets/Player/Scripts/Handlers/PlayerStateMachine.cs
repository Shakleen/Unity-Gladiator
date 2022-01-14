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
        _states[GetIndex(PlayerStateType.idle)] = new PlayerIdleState(_player, this);
        _states[GetIndex(PlayerStateType.walk)] = new PlayerWalkState(_player, this);
        _states[GetIndex(PlayerStateType.run)] = new PlayerRunState(_player, this);
        _states[GetIndex(PlayerStateType.dodge)] = new PlayerDodgeState(_player, this);
        _states[GetIndex(PlayerStateType.melee_idle)] = new PlayerIdleMeleeAttackState(_player, this);
        _states[GetIndex(PlayerStateType.melee_walking)] = new PlayerWalkingMeleeAttackState(_player, this);
        _states[GetIndex(PlayerStateType.melee_running)] = new PlayerRunningMeleeAttackState(_player, this);
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