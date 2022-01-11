using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(Player context, PlayerStateManager manager) : base(context, manager) {}

    public override void OnEnterState() { hasPrint = false; }

    public override void OnExitState() { hasPrint = false; }

    public override void CheckSwitchState() 
    {
        if (_context.InputHandler.IsInputActiveMovement)
            SwitchState(_manager.GetWalkState());
        else if (_context.InputHandler.IsInputActiveDodge)
            SwitchState(_manager.GetDodgeState());
        else if (_context.InputHandler.IsInputActiveAttack)
            SwitchState(_manager.GetMeleeAttackState());
    }

    public override void ExecuteState() 
    { 
        CheckSwitchState(); 
        if (HasMovementVelocity()) Decelarate();
    }

    private bool HasMovementVelocity()
    {
        bool hasVelocityX = Mathf.Abs(_context.MovementHandler.CurrentMovementVelocityX) > _context.MovementHandler.THRESH;
        bool hasVelocityZ = Mathf.Abs(_context.MovementHandler.CurrentMovementVelocityZ) > _context.MovementHandler.THRESH;
        return hasVelocityX || hasVelocityZ;
    }

    private void Decelarate()
    {
        float velocityX = DecelarateAlongAxis(_context.MovementHandler.CurrentMovementVelocityX);
        _context.MovementHandler.CurrentMovementVelocityX = velocityX;

        float velocityZ = DecelarateAlongAxis(_context.MovementHandler.CurrentMovementVelocityZ);
        _context.MovementHandler.CurrentMovementVelocityZ = velocityZ;
    }

    private float DecelarateAlongAxis(float currentVelocity)
    {
        float velocity = currentVelocity;

        if (currentVelocity > _context.MovementHandler.THRESH)
            velocity = currentVelocity - ApplyFrameIndependentDecelaration();
        
        // Player hasn't pressed any button for this axis but was moving in the negative direction.
        else if (currentVelocity < -_context.MovementHandler.THRESH)
            velocity = currentVelocity + ApplyFrameIndependentDecelaration();
        
        else
            velocity = 0;

        velocity = Mathf.Clamp(velocity, -_context.MovementHandler.MaxVelocityRun, _context.MovementHandler.MaxVelocityRun);
        return velocity;
    }

    private float ApplyFrameIndependentDecelaration() { return _context.MovementHandler.DecelarationRun * Time.deltaTime; }

    public override string GetName()
    {
        hasPrint = true;
        return "Idle";
    }
}