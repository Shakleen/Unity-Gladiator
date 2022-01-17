using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    private bool _animationPlaying;
    private bool _isDodging;

    public PlayerDodgeState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

    public override void InitializeTransitions()
    {
        _transtions.Add(new Transition(PlayerStateType.run, ToRunCondition));
        _transtions.Add(new Transition(PlayerStateType.walk, ToWalkCondition));
        _transtions.Add(new Transition(PlayerStateType.idle, ToIdleCondition));
    }

    private new bool ToRunCondition() => NotDodgingAndDodgeNotPressed() && base.ToRunCondition();

    private bool ToWalkCondition()
    {
        _movePressed = _player.InputHandler.IsInputActiveMovement;
        _runPressed = _player.InputHandler.IsInputActiveRun;
        return NotDodgingAndDodgeNotPressed() && _movePressed && !_runPressed;
    }

    private bool ToIdleCondition()
    {
        _movePressed = _player.InputHandler.IsInputActiveMovement;
        _runPressed = _player.InputHandler.IsInputActiveRun;
        return NotDodgingAndDodgeNotPressed() && !_movePressed && !_runPressed;
    }

    private bool NotDodgingAndDodgeNotPressed()
    {
        _dodgePressed = _player.InputHandler.IsInputActiveDodge;
        _isDodging = _player.AnimatorHandler.IsDodging;
        return !_dodgePressed && !_isDodging;
    }

    public override PlayerStateType GetStateType() => PlayerStateType.dodge;

    public override void OnEnterState() {}

    public override void ExecuteState()
    {
        CheckSwitchState();
        _player.MovementHandler.FaceDodgeDirection();
    }
}