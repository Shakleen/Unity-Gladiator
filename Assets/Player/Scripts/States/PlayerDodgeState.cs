using System;

public class PlayerDodgeState : PlayerBaseState
{
    private bool _isDodging;
    private Transition _toRun, _toWalk, _toIdle;

    public PlayerDodgeState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) 
    {
        _toRun = new Transition(GetStateType(), PlayerStateEnum.run);
        _toWalk = new Transition(GetStateType(), PlayerStateEnum.walk);
        _toIdle = new Transition(GetStateType(), PlayerStateEnum.idle);
    }

    public override Enum GetStateType() => PlayerStateEnum.dodge;

    public override void OnEnterState(Transition transition) {}

    public override void ExecuteState()
    {
        CheckSwitchState();
        _player.MovementHandler.FaceDodgeDirection();
    }

    public override Transition GetTransition()
    {
        if (NoDodgeActivity())
        {
            if (isMovePressed())
            {
                if (hasStamina() && isRunPressed())
                    return _toRun;
                else
                    return _toWalk;
            }

            return _toIdle;
        }

        return null;
    }

    private bool NoDodgeActivity()
    {
        _dodgePressed = _player.InputHandler.IsInputActiveDodge;
        _isDodging = _player.AnimatorHandler.IsDodging;
        return !_dodgePressed && !_isDodging;
    }

    public override void OnExitState(Transition transition) {}
}