using System.Collections.Generic;

public class PlayerStateManager
{
    enum State {idle, walk, run, dodge, melee_idle, melee_walking, melee_running}

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

    public PlayerBaseState GetIdleMeleeAttackState()
    {
        if (!_stateDict.ContainsKey(State.melee_idle))
            _stateDict.Add(State.melee_idle, new PlayerIdleMeleeAttackState(_context, this));

        return _stateDict[State.melee_idle];
    }

    public PlayerBaseState GetWalkingMeleeAttackState()
    {
        if (!_stateDict.ContainsKey(State.melee_walking))
            _stateDict.Add(State.melee_walking, new PlayerWalkingMeleeAttackState(_context, this));

        return _stateDict[State.melee_walking];
    }

    public PlayerBaseState GetRunningMeleeAttackState()
    {
        if (!_stateDict.ContainsKey(State.melee_running))
            _stateDict.Add(State.melee_running, new PlayerRunningMeleeAttackState(_context, this));

        return _stateDict[State.melee_running];
    }
}