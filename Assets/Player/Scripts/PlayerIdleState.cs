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
    }

    public override void ExecuteState() { CheckSwitchState(); }

    public override string GetName()
    {
        hasPrint = true;
        return "Idle";
    }
}