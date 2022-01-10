public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerController context, PlayerStateManager manager) : base(context, manager) {}

    public override void OnEnterState() { hasPrint = false; }

    public override void OnExitState() { hasPrint = false; }

    public override void CheckSwitchState() {}

    public override void ExecuteState() {}

    public override string GetName()
    {
        hasPrint = true;
        return "Idle";
    }
}