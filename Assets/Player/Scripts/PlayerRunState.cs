using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerController context, PlayerStateManager manager) : base(context, manager) {}

    public override void OnEnterState() { hasPrint = false; }

    public override void OnExitState() { hasPrint = false; }

    public override void CheckSwitchState() 
    {
        if (!_context.IsInputActiveRun)
            SwitchState(_manager.GetWalkState());
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

        if (inputVelocity > 0)
        {
            // Player pressed button to move in the positive direction of axis
            velocity = currentVelocity + ApplyFrameIndependentAccelaration(inputVelocity);
            velocity = Mathf.Clamp(velocity, 0, _context.MaxVelocityRun);
        }
        else if (inputVelocity < 0)
        {
            // Player pressed button to move in the negative direction of axis
            velocity = currentVelocity - ApplyFrameIndependentAccelaration(inputVelocity);
            velocity = Mathf.Clamp(velocity, _context.MaxBackwardWalkVelocity, 0);
        }
        else
        {
            if (currentVelocity > 0)
            {
                // Player hasn't pressed any button for this axis but was moving in the positive direction.
                velocity = currentVelocity - ApplyFrameIndependentDecelaration();
                velocity = Mathf.Clamp(velocity, 0, _context.MaxVelocityRun);
            }
            else if (currentVelocity < 0)
            {
                // Player hasn't pressed any button for this axis but was moving in the negative direction.
                velocity = currentVelocity + ApplyFrameIndependentDecelaration();
                velocity = Mathf.Clamp(velocity, _context.MaxBackwardWalkVelocity, 0);
            }
        }

        return velocity;
    }

    private float ApplyFrameIndependentAccelaration(float inputVelocity) { return _context.AccelarationRun * Mathf.Abs(inputVelocity) * Time.deltaTime; }

    private float ApplyFrameIndependentDecelaration() { return _context.DecelarationRun * Time.deltaTime; }

    public override string GetName()
    {
        hasPrint = true;
        return "Run";
    }
}