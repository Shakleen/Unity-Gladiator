public enum PlayerStateEnum { idle, walk, run, dodge, melee_idle, melee_walking, melee_running, death }

public abstract class PlayerBaseState : BaseStateClass
{
    protected Player _player;
    protected PlayerStateMachine _stateMachine;
    protected bool _hasStamina;
    protected bool _dodgePressed, _attackPressed, _movePressed, _runPressed;
    protected float _velocityX, _velocityZ;

    protected PlayerBaseState(Player player, PlayerStateMachine stateMachine)
    {
        _player = player;
        _stateMachine = stateMachine;
    }

    public override void CheckSwitchState()
    {
        Transition _transition = GetTransition();
        if (_transition != null)
            _stateMachine.SwitchState(_transition);
    }

    #region Common transition conditions
    protected bool IsDeath() => _hasStamina = _player.StatusHandler.Health.IsEmpty();
    protected bool hasStamina() => _hasStamina = !_player.StatusHandler.Stamina.IsEmpty();
    protected bool isRunPressed() => _player.InputHandler.IsInputActiveRun;
    protected bool isMovePressed() => _player.InputHandler.IsInputActiveMovement;
    protected bool isDodgePressed() => _player.InputHandler.IsInputActiveDodge;
    protected bool isAttackPressed() => _player.InputHandler.IsInputActiveMeleeAttack;
    #endregion
}