using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    private Vector2 _dodgeDirection;
    public PlayerDodgeState(Player context, PlayerStateManager manager) : base(context, manager) {}

    public override void OnEnterState() 
    { 
        hasPrint = false;
        _dodgeDirection = _context.InputHandler.InputMoveVector;
        _context.AnimatorHandler.SetAnimationValueIsDodging(true);
    }

    public override void OnExitState() 
    {  
        hasPrint = false; 
        _context.AnimatorHandler.SetAnimationValueIsDodging(false);
    }

    public override void CheckSwitchState() 
    {
        if (!_context.AnimatorHandler.IsDodging)
        {
            if (_context.InputHandler.IsInputActiveMovement)
            {
                if (_context.InputHandler.IsInputActiveRun)
                    SwitchState(_manager.GetRunState());
                else
                    SwitchState(_manager.GetWalkState());
            }
            else if (!_context.AnimatorHandler.IsAnimationPlaying())
            {
                SwitchState(_manager.GetIdleState());
            }
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
        movementVector = _context.MovementHandler.GetMoveVectorTowardsCameraDirection(movementVector);
        Quaternion movementDirection = Quaternion.LookRotation(new Vector3(movementVector.x, 0, movementVector.z));
        _context.transform.rotation = Quaternion.Slerp(
            _context.transform.rotation,
            movementDirection,
            _context.MovementHandler.CameraSensitivity * Time.deltaTime
        );
    }

    public override string GetName()
    {
        hasPrint = true;
        return "Dodge";
    }
}