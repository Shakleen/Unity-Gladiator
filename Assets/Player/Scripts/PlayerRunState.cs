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

    public override void ExecuteState() { CheckSwitchState(); }

    public override string GetName()
    {
        hasPrint = true;
        return "Run";
    }
}