using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    protected const float THRESH = 1e-3f;

    public PlayerWalkState(Player context, PlayerStateManager manager) : base(context, manager) {}

    public override void OnEnterState() 
    { 
        hasPrint = false; 
        _context.AnimatorHandler.SetAnimationValueIsMoving(true);
    }

    public override void OnExitState() { hasPrint = false; }

    public override void CheckSwitchState() 
    {
        if (_context.InputHandler.IsInputActiveDodge)
            SwitchState(_manager.GetDodgeState());
        else if (_context.InputHandler.IsInputActiveAttack)
            SwitchState(_manager.GetMeleeAttackState());
        else if (_context.InputHandler.IsInputActiveRun)
            SwitchState(_manager.GetRunState());
        else if (!_context.InputHandler.IsInputActiveMovement && !HasWalkVelocity())
        {
            SwitchState(_manager.GetIdleState());
            _context.AnimatorHandler.SetAnimationValueIsMoving(false);
        }
    }

    private bool HasWalkVelocity()
    {
        bool hasRunVelocityX = Mathf.Abs(_context.MovementHandler.CurrentMovementVelocityX) > THRESH;
        bool hasRunVelocityZ = Mathf.Abs(_context.MovementHandler.CurrentMovementVelocityZ) > THRESH;
        return hasRunVelocityX || hasRunVelocityZ;
    }

    public override void ExecuteState() 
    { 
        CheckSwitchState(); 

        float velocityX = ChangeAxisVelocity(_context.InputHandler.InputMoveVector.x, _context.MovementHandler.CurrentMovementVelocityX);
        _context.MovementHandler.CurrentMovementVelocityX = velocityX;

        float velocityZ = ChangeAxisVelocity(_context.InputHandler.InputMoveVector.y, _context.MovementHandler.CurrentMovementVelocityZ);
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

        velocity = Mathf.Clamp(
            velocity, 
            -_context.MovementHandler.MaxForwardWalkVelocity, 
            _context.MovementHandler.MaxForwardWalkVelocity
        );
        return velocity;
    }

    private float ApplyFrameIndependentAccelaration() { return _context.MovementHandler.AccelarationWalk * Time.deltaTime; }

    private float ApplyFrameIndependentDecelaration() { return _context.MovementHandler.DecelarationWalk * Time.deltaTime; }

    public override string GetName()
    {
        hasPrint = true;
        return "Walk";
    }
}