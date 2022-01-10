using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    protected const float THRESH = 1e-3f;

    public PlayerWalkState(PlayerController context, PlayerStateManager manager) : base(context, manager) {}

    public override void OnEnterState() { hasPrint = false; }

    public override void OnExitState() { hasPrint = false; }

    public override void CheckSwitchState() 
    {
        if (!_context.IsInputActiveMovement && !HasWalkVelocity())
            SwitchState(_manager.GetIdleState());
        else if (_context.IsInputActiveRun)
            SwitchState(_manager.GetRunState());
        else if (_context.IsInputActiveDodge)
            SwitchState(_manager.GetDodgeState());
    }

    private bool HasWalkVelocity()
    {
        bool hasRunVelocityX = Mathf.Abs(_context.CurrentMovementVelocityX) > THRESH;
        bool hasRunVelocityZ = Mathf.Abs(_context.CurrentMovementVelocityZ) > THRESH;
        return hasRunVelocityX || hasRunVelocityZ;
    }

    public override void ExecuteState() 
    { 
        CheckSwitchState(); 
        _context.CurrentMovementVelocityX = ChangeAxisVelocity(_context.InputMovementVector.x, _context.CurrentMovementVelocityX);
        _context.CurrentMovementVelocityZ = ChangeAxisVelocity(_context.InputMovementVector.y, _context.CurrentMovementVelocityZ);
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

        velocity = Mathf.Clamp(velocity, -_context.MaxForwardWalkVelocity, _context.MaxForwardWalkVelocity);
        return velocity;
    }

    private float ApplyFrameIndependentAccelaration() { return _context.AccelarationWalk * Time.deltaTime; }

    private float ApplyFrameIndependentDecelaration() { return _context.DecelarationWalk * Time.deltaTime; }

    public override string GetName()
    {
        hasPrint = true;
        return "Walk";
    }
}