using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    private Vector2 _dodgeDirection;
    public PlayerDodgeState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

    public override PlayerStateType GetStateType() { return PlayerStateType.dodge; }

    public override void OnEnterState() 
    { 
        _dodgeDirection = _player.InputHandler.InputMoveVector;
        _player.AnimatorHandler.SetAnimationValueIsDodging(true);
        _player.StatusHandler.UseStamina(_player.Config.staminaCost.dodge);
    }

    public override void OnExitState() { _player.AnimatorHandler.SetAnimationValueIsDodging(false); }

    public override void CheckSwitchState() 
    {
        if (!_player.AnimatorHandler.IsDodging && !_player.InputHandler.IsInputActiveDodge)
        {
            if (_player.InputHandler.IsInputActiveMovement)
            {
                if (_player.InputHandler.IsInputActiveRun)
                    _stateMachine.SwitchState(PlayerStateType.run);
                else
                    _stateMachine.SwitchState(PlayerStateType.walk);
            }
            else if (!_player.AnimatorHandler.IsAnimationPlaying())
                _stateMachine.SwitchState(PlayerStateType.idle);
        }
    }

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
                _player.Config.CameraSensitivity * Time.deltaTime
            );
        }
    }
}