public class PlayerDodgeState : PlayerBaseState
{
    public PlayerDodgeState(PlayerController context, PlayerStateManager manager) : base(context, manager) {}

    public override void OnEnterState() 
    { 
        hasPrint = false;
        _context.Animator.SetBool(_context.AnimatorHashIsDodging, true);
    }

    public override void OnExitState() 
    {  
        hasPrint = false; 
        _context.Animator.SetBool(_context.AnimatorHashIsDodging, false);
    }

    public override void CheckSwitchState() 
    {
        if (_context.IsInputActiveDodge || _context.IsDodging)
            return;
        else if (!_context.IsInputActiveMovement)
            SwitchState(_manager.GetIdleState());
        else if (_context.IsInputActiveRun)
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