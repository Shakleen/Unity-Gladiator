using System;
using System.Collections.Generic;

public abstract class BaseState
{
    protected readonly struct Transition
    {
        public Transition(Enum destination, Func<bool> Condition, System.Action OnExit = null)
        {
            this.destination = destination;
            this.Condition = Condition;
            this.OnExit = OnExit;
        }

        public Func<bool> Condition { get; }
        public Enum destination { get; }
        public System.Action OnExit { get; }
    }

    protected List<Transition> _transtions;

    protected BaseState()
    {
        _transtions = new List<Transition>();
        InitializeTransitions();
    }

    
    #region Abstract methods
    public abstract void InitializeTransitions();
    public abstract void OnEnterState();
    public abstract Enum GetStateType();
    public abstract void ExecuteState();
    #endregion
}