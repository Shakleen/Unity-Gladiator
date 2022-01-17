using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    private Vector2 _dodgeDirection;
    private bool _animationPlaying;
    private bool _isDodging;

    public PlayerDodgeState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

    public override void InitializeTransitions()
    {
        _transtions.Add(new Transition(PlayerStateType.run, ToRunCondition, OnExitState));
        _transtions.Add(new Transition(PlayerStateType.run, ToWalkCondition, OnExitState));
        _transtions.Add(new Transition(PlayerStateType.idle, ToIdleCondition, OnExitState));
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
        _animationPlaying = _player.AnimatorHandler.IsAnimationPlaying();
        return NotDodgingAndDodgeNotPressed() && !_movePressed && !_runPressed && _animationPlaying;
    }

    private bool NotDodgingAndDodgeNotPressed()
    {
        _dodgePressed = _player.InputHandler.IsInputActiveDodge;
        _isDodging = _player.AnimatorHandler.IsDodging;
        return !_dodgePressed && !_isDodging;
    }

    public override PlayerStateType GetStateType() => PlayerStateType.dodge;

    public override void OnEnterState() 
    { 
        _dodgeDirection = _player.InputHandler.InputMoveVector;
        _player.AnimatorHandler.SetAnimationValueIsDodging(true);
        _player.StatusHandler.UseStamina(_player.Config.staminaCost.dodge);
    }

    private void OnExitState() => _player.AnimatorHandler.SetAnimationValueIsDodging(false);

    public override void ExecuteState()
    {
        CheckSwitchState();
        FaceDodgeDirection();
    }

    private void FaceDodgeDirection()
    {
        Vector3 movementVector = new Vector3(_dodgeDirection.x, 0, _dodgeDirection.y);
        movementVector = _player.MovementHandler.GetMoveVectorTowardsCameraDirection(movementVector);
        movementVector.y = 0;

        if (movementVector != Vector3.zero)
        {
            Quaternion movementDirection = Quaternion.LookRotation(movementVector);
            _player.transform.rotation = Quaternion.Slerp(
                _player.transform.rotation,
                movementDirection,
                _player.Config.misc.cameraSensitivity * Time.deltaTime
            );
        }
    }
}