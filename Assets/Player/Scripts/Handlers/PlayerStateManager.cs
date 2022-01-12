using System.Collections.Generic;

public class PlayerStateManager
{
    enum State {idle, walk, run, dodge, melee}

    private Player _context;
    private Dictionary<State, PlayerBaseState> _stateDict;

    public PlayerStateManager(Player context)
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

    public PlayerBaseState GetWalkState()
    {
        if (!_stateDict.ContainsKey(State.walk))
            _stateDict.Add(State.walk, new PlayerWalkState(_context, this));

        return _stateDict[State.walk];
    }

    public PlayerBaseState GetRunState()
    {
        if (!_stateDict.ContainsKey(State.run))
            _stateDict.Add(State.run, new PlayerRunState(_context, this));

        return _stateDict[State.run];
    }

    public PlayerBaseState GetDodgeState()
    {
        if (!_stateDict.ContainsKey(State.dodge))
            _stateDict.Add(State.dodge, new PlayerDodgeState(_context, this));

        return _stateDict[State.dodge];
    }

    public PlayerBaseState GetMeleeAttackState()
    {
        if (!_stateDict.ContainsKey(State.melee))
            _stateDict.Add(State.melee, new PlayerMeleeAttackState(_context, this));

        return _stateDict[State.melee];
    }
}