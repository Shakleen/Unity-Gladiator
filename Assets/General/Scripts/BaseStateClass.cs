using System;

public class Transition
{
    public Transition(Enum source, Enum destination)
    {
        this.source = source;
        this.destination = destination;
    }

    public Enum source { get; }
    public Enum destination { get; }
}

public abstract class BaseStateClass
{
    #region Abstract methods
    public abstract void OnEnterState(Transition transition);
    public abstract void ExecuteState();
    public abstract void OnExitState(Transition transition);
    public abstract Enum GetStateType();
    public abstract void CheckSwitchState();
    public abstract Transition GetTransition();
    #endregion
}