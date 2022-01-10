using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    protected const float THRESH = 1e-3f;
    
    public PlayerRunState(PlayerController context, PlayerStateManager manager) : base(context, manager) {}

    public override void OnEnterState() { hasPrint = false; }

    public override void OnExitState() { hasPrint = false; }

    public override void CheckSwitchState()
    {
        if (!_context.IsInputActiveRun && !HasRunVelocity())
            SwitchState(_manager.GetWalkState());
    }

    private bool HasRunVelocity()
    {
        bool hasRunVelocityX = Mathf.Abs(_context.CurrentMovementVelocityX) > _context.MaxForwardWalkVelocity;
        bool hasRunVelocityZ = Mathf.Abs(_context.CurrentMovementVelocityZ) > _context.MaxForwardWalkVelocity;
        return hasRunVelocityX || hasRunVelocityZ;
    }

    public override void ExecuteState() 
    { 
        CheckSwitchState();

        _context.CurrentMovementVelocityX = ChangeAxisVelocity(
            _context.InputMovementVector.x, 
            _context.CurrentMovementVelocityX,
            -_context.MaxVelocityRun,
            _context.MaxVelocityRun
        );

        _context.CurrentMovementVelocityZ = ChangeAxisVelocity(
            _context.InputMovementVector.y, 
            _context.CurrentMovementVelocityZ,
            -_context.MaxForwardWalkVelocity,
            _context.MaxVelocityRun
        );
    }

    private float ChangeAxisVelocity(float inputVelocity, float currentVelocity, float minValue, float maxValue)
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

        velocity = Mathf.Clamp(velocity, minValue, maxValue);
        return velocity;
    }

    private float ApplyFrameIndependentAccelaration() { return _context.AccelarationRun * Time.deltaTime; }

    private float ApplyFrameIndependentDecelaration() { return _context.DecelarationRun * Time.deltaTime; }

    public override string GetName()
    {
        hasPrint = true;
        return "Run";
    }
}