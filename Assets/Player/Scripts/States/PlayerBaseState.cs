using System.Collections.Generic;
using UnityEngine;

public enum PlayerStateType { idle, walk, run, dodge, melee_idle, melee_walking, melee_running }

public abstract class PlayerBaseState : BaseState
{
    protected Player _player;
    protected PlayerStateMachine _stateMachine;
    protected bool _hasStamina;
    protected bool _dodgePressed, _attackPressed, _movePressed, _runPressed;
    protected float _velocityX, _velocityZ;

    protected PlayerBaseState(Player player, PlayerStateMachine stateMachine)
    {
        _player = player;
        _stateMachine = stateMachine;
    }

    public void CheckSwitchState() 
    {
        foreach(Transition transition in _transtions)
        {
            if (transition.Condition())
            {
                if (transition.OnExit != null) transition.OnExit();
                _stateMachine.SwitchState((PlayerStateType) transition.destination);
                return;
            }
        }
    }

    #region Common transition conditions
    protected bool ToDodgeCondition()
    {
        _hasStamina = !_player.StatusHandler.Stamina.IsEmpty();
        _dodgePressed = _player.InputHandler.IsInputActiveDodge;
        return _hasStamina && _dodgePressed;
    }

    protected bool ToMeleeCondition()
    {
        _hasStamina = !_player.StatusHandler.Stamina.IsEmpty();
        _attackPressed = _player.InputHandler.IsInputActiveMeleeAttack;
        return _hasStamina && _attackPressed;
    }

    protected bool ToRunCondition()
    {
        _hasStamina = !_player.StatusHandler.Stamina.IsEmpty();
        _movePressed = _player.InputHandler.IsInputActiveMovement; 
        _runPressed = _player.InputHandler.IsInputActiveRun;
        return _hasStamina && _movePressed && _runPressed;
    }
    #endregion
}