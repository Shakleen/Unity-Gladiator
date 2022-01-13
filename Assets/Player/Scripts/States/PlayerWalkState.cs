using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    protected const float THRESH = 1e-3f;

    public PlayerWalkState(Player context, PlayerStateManager manager) : base(context, manager) {}

    public override void OnEnterState() 
    { 
        hasPrint = false; 
        _context.AnimatorHandler.SetAnimationValueIsMoving(true);
        _context.AnimatorHandler.SetAnimationValueIsRunning(false);
        _context.AnimatorHandler.SetAnimationValueIsDodging(false);
        _context.AnimatorHandler.SetAnimationValueIsMeleeAttacking(false);
    }

    public override void OnExitState() { hasPrint = false; }

    public override void CheckSwitchState() 
    {
        if (_context.InputHandler.IsInputActiveDodge)
            SwitchState(_manager.GetDodgeState());
        else if (_context.InputHandler.IsInputActiveMeleeAttack)
            SwitchState(_manager.GetWalkingMeleeAttackState());
        else if (!_context.InputHandler.IsInputActiveMovement)
        {
            SwitchState(_manager.GetIdleState());
            _context.AnimatorHandler.SetAnimationValueIsMoving(false);
        }
        else if (_context.InputHandler.IsInputActiveRun)
            SwitchState(_manager.GetRunState());
    }

    public override void ExecuteState()
    {
        CheckSwitchState();
        _context.MovementHandler.RotateTowardsCameraDirection();
        UpdateWalkVelocity();
        _context.MovementHandler.MoveCharacter();
    }

    private void UpdateWalkVelocity()
    {
        Vector2 inputMoveVector = _context.InputHandler.InputMoveVector;

        float velocityX = _context.MovementHandler.CurrentMovementVelocityX;
        velocityX = ChangeAxisVelocity(inputMoveVector.x, velocityX);
        _context.MovementHandler.CurrentMovementVelocityX = velocityX;

        float velocityZ = _context.MovementHandler.CurrentMovementVelocityZ;
        velocityZ = ChangeAxisVelocity(inputMoveVector.y, velocityZ);
        _context.MovementHandler.CurrentMovementVelocityZ = velocityZ;
    }

    private float ChangeAxisVelocity(float inputVelocity, float currentVelocity)
    {
        float velocity = currentVelocity;

        // Player pressed button to move in the positive direction of axis
        if (inputVelocity > 0)
            velocity = currentVelocity + ApplyFrameIndependentAccelaration();
        
        // Player pressed button to move in the negative direction of axis
        else if (inputVelocity < 0)
            velocity = currentVelocity - ApplyFrameIndependentAccelaration();
        
        // Player hasn't pressed any button for this axis but was moving in the positive direction.
        else if (currentVelocity > THRESH)
            velocity = currentVelocity - ApplyFrameIndependentDecelaration();
        
        // Player hasn't pressed any button for this axis but was moving in the negative direction.
        else if (currentVelocity < -THRESH)
            velocity = currentVelocity + ApplyFrameIndependentDecelaration();
        
        else
            velocity = 0;

        velocity = Mathf.Clamp(velocity, -_context.Config.MaxWalkVelocity, _context.Config.MaxWalkVelocity);
        return velocity;
    }

    private float ApplyFrameIndependentAccelaration() { return _context.Config.AccelarationWalk * Time.deltaTime; }

    private float ApplyFrameIndependentDecelaration() { return _context.Config.DecelarationWalk * Time.deltaTime; }

    public override string GetName()
    {
        hasPrint = true;
        return "Walk";
    }
}