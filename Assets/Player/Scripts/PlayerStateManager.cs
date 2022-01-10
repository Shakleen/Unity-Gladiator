using System.Collections.Generic;

public class PlayerStateManager
{
    enum State {idle, walk, run, dodge}

    private PlayerController _context;
    private Dictionary<State, PlayerBaseState> _stateDict;

    public PlayerStateManager(PlayerController context) 
    {
        _context = context;
        _stateDict = new Dictionary<State, PlayerBaseState>();
    }

    public PlayerBaseState GetIdleState()
    {
        if (!_stateDict.ContainsKey(State.idle))
            _stateDict.Add(State.idle, new PlayerIdleState(_context, this));

        return _stateDict[State.idle];
    }
}