public abstract class PlayerBaseState
{
    protected PlayerController _context;
    protected PlayerStateManager _manager;
    public bool hasPrint; //TODO: Remove when done

    protected PlayerBaseState(PlayerController context, PlayerStateManager manager)
    {
        _context = context;
        _manager = manager;
    }

    public abstract void OnEnterState();

    public abstract void OnExitState();

    public abstract void CheckSwitchState();

    public abstract void ExecuteState();

    public abstract string GetName(); //TODO: Remove when done

    protected void SwitchState(PlayerBaseState newState)
    {
        OnExitState();
        newState.OnEnterState();
        _context.CurrentState = newState;
    }
}