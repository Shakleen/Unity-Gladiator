public class PlayerDodgeState : PlayerBaseState
{
    public PlayerDodgeState(Player context, PlayerStateManager manager) : base(context, manager) {}

    public override void OnEnterState() 
    { 
        hasPrint = false;
        _context.AnimatorHandler.SetAnimationValueIsDodging(true);
    }

    public override void OnExitState() 
    {  
        hasPrint = false; 
        _context.AnimatorHandler.SetAnimationValueIsDodging(false);
    }

    public override void CheckSwitchState() 
    {
        if (_context.InputHandler.IsInputActiveDodge || _context.AnimatorHandler.IsDodging)
            return;
        else if (!_context.InputHandler.IsInputActiveMovement)
            SwitchState(_manager.GetIdleState());
        else if (_context.InputHandler.IsInputActiveRun)
            SwitchState(_manager.GetRunState());
        else
            SwitchState(_manager.GetWalkState());
    }

    public override void ExecuteState() { CheckSwitchState(); }

    public override string GetName()
    {
        hasPrint = true;
        return "Dodge";
    }
}