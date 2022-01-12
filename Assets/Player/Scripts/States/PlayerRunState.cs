using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(Player context, PlayerStateManager manager) : base(context, manager) {}

    public override void OnEnterState() 
    { 
        hasPrint = false; 
        _context.AnimatorHandler.SetAnimationValueIsRunning(true);
    }

    public override void OnExitState() { hasPrint = false; }

    public override void CheckSwitchState()
    {
        if (_context.InputHandler.IsInputActiveDodge)
            SwitchState(_manager.GetDodgeState());
        else if (_context.InputHandler.IsInputActiveMeleeAttack)
            SwitchState(_manager.GetRunningMeleeAttackState());
        else if (!_context.InputHandler.IsInputActiveRun && !HasRunVelocity())
        {
            _context.AnimatorHandler.SetAnimationValueIsRunning(false);
            SwitchState(_manager.GetWalkState());
        }
    }

    private bool HasRunVelocity()
    {
        bool hasRunVelocityX = Mathf.Abs(_context.MovementHandler.CurrentMovementVelocityX) > _context.MovementHandler.MaxForwardWalkVelocity;
        bool hasRunVelocityZ = Mathf.Abs(_context.MovementHandler.CurrentMovementVelocityZ) > _context.MovementHandler.MaxForwardWalkVelocity;
        return hasRunVelocityX || hasRunVelocityZ;
    }

    public override void ExecuteState() 
    { 
        CheckSwitchState();
        _context.MovementHandler.RotateTowardsCameraDirection();

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
        else if (currentVelocity > _context.MovementHandler.THRESH)
            velocity = currentVelocity - ApplyFrameIndependentDecelaration();
        
        // Player hasn't pressed any button for this axis but was moving in the negative direction.
        else if (currentVelocity < -_context.MovementHandler.THRESH)
            velocity = currentVelocity + ApplyFrameIndependentDecelaration();
        
        else
            velocity = 0;

        velocity = Mathf.Clamp(velocity, -_context.MovementHandler.MaxVelocityRun, _context.MovementHandler.MaxVelocityRun);
        return velocity;
    }

    private float ApplyFrameIndependentAccelaration() { return _context.MovementHandler.AccelarationRun * Time.deltaTime; }

    private float ApplyFrameIndependentDecelaration() { return _context.MovementHandler.DecelarationRun * Time.deltaTime; }

    public override string GetName()
    {
        hasPrint = true;
        return "Run";
    }
}