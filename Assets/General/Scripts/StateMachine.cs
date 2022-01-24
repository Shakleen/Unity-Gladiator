using UnityEngine;

public class StateMachine<T> : MonoBehaviour 
{
    [SerializeField] private T _context;
    [SerializeField] private BaseState<T> _currentState;

    public void Execute()
    {
        SwitchState();
        _currentState.ExecuteState(_context);
    }

    private void SwitchState()
    {
        foreach(BaseState<T> destination in _currentState.destinations)
        {
            if (destination.EntryCondition(_context))
            {
                _currentState.OnExitState(_context);
                _currentState = destination;
                _currentState.OnEnterState(_context);
                return;
            }
        }
    }
}