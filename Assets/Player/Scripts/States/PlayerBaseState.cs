public enum PlayerStateType { idle, walk, run, dodge, melee_idle, melee_walking, melee_running }

public abstract class PlayerBaseState
{
    protected Player _player;
    protected PlayerStateMachine _stateMachine;

    protected PlayerBaseState(Player player, PlayerStateMachine stateMachine)
    {
        _player = player;
        _stateMachine = stateMachine;
    }

    public abstract PlayerStateType GetStateType();

    public abstract void OnEnterState();

    public abstract void OnExitState();

    public abstract void CheckSwitchState();

    public abstract void ExecuteState();
}