using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<T> : ScriptableObject 
{
    [SerializeField] protected List<BaseState<T>> _destinationStates;
    public List<BaseState<T>> destinations => _destinationStates;

    public abstract bool EntryCondition(T context);

    public abstract void OnEnterState(T context);
    
    public abstract void ExecuteState(T context);
    
    public abstract void OnExitState(T context);
}