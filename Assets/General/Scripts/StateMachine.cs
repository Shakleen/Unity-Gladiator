using UnityEngine;

public class StateMachine<T> : MonoBehaviour 
{
    [SerializeField] protected T _context;
    [SerializeField] protected BaseState<T> _currentState;
    [SerializeField] private BaseState<T> _startState;

    private void OnEnable() => _currentState = _startState;

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