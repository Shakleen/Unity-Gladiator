using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    private const float THRESH = 1e-5f;

    public PlayerWalkState(PlayerController context, PlayerStateManager manager) : base(context, manager) {}

    public override void OnEnterState() { hasPrint = false; }

    public override void OnExitState() { hasPrint = false; }

    public override void CheckSwitchState() 
    {
        if (!_context.IsInputActiveMovement && _context.CurrentMovementVelocity == Vector3.zero)
            SwitchState(_manager.GetIdleState());
        else if (_context.IsInputActiveRun)
            SwitchState(_manager.GetRunState());
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
            velocity = Mathf.Clamp(velocity, 0, MaxAllowablePositiveVelocity(inputVelocity));
        }
        else if (inputVelocity < 0)
        {
            // Player pressed button to move in the negative direction of axis
            velocity = currentVelocity - ApplyFrameIndependentAccelaration(inputVelocity);
            velocity = Mathf.Clamp(velocity, MaxAllowableNegativeVelocity(inputVelocity), 0);
        }
        else
        {
            if (currentVelocity > 0)
            {
                // Player hasn't pressed any button for this axis but was moving in the positive direction.
                velocity = currentVelocity - ApplyFrameIndependentDecelaration();
                velocity = Mathf.Clamp(velocity, 0, _context.MaxForwardWalkVelocity);
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

    private float MaxAllowableNegativeVelocity(float inputVelocity) { return _context.MaxBackwardWalkVelocity * Mathf.Abs(inputVelocity); }

    private float MaxAllowablePositiveVelocity(float inputVelocity) { return _context.MaxForwardWalkVelocity * Mathf.Abs(inputVelocity); }

    private float ApplyFrameIndependentAccelaration(float inputVelocity) { return _context.AccelarationWalk * Mathf.Abs(inputVelocity) * Time.deltaTime; }

    private float ApplyFrameIndependentDecelaration() { return _context.DecelarationWalk * Time.deltaTime; }

    private static bool IsVelocityZero(float inputVelocity) { return (inputVelocity > -THRESH) || (inputVelocity < THRESH); }

    public override string GetName()
    {
        hasPrint = true;
        return "Walk";
    }
}