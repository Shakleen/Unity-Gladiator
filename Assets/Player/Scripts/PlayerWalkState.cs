public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerController context, PlayerStateManager manager) : base(context, manager) {}

    public override void OnEnterState() { hasPrint = false; }

    public override void OnExitState() { hasPrint = false; }

    public override void CheckSwitchState() 
    {
        if (!_context.IsInputActiveMovement)
            SwitchState(_manager.GetIdleState());
        else if (_context.IsInputActiveRun)
            SwitchState(_manager.GetRunState());
    }

    public override void ExecuteState() { CheckSwitchState(); }

    public override string GetName()
    {
        hasPrint = true;
        return "Walk";
    }
}