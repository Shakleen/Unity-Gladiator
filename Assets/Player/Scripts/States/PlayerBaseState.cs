using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStateType { idle, walk, run, dodge, melee_idle, melee_walking, melee_running }

public abstract class PlayerBaseState
{
    protected readonly struct Transition
    {
        public Transition(PlayerStateType destination, Func<bool> Condition, System.Action OnExit = null)
        {
            this.destination = destination;
            this.Condition = Condition;
            this.OnExit = OnExit;
        }

        public Func<bool> Condition { get; }
        public PlayerStateType destination { get; }
        public System.Action OnExit { get; }
    }
    protected Player _player;
    protected PlayerStateMachine _stateMachine;
    protected List<Transition> _transtions;
    protected bool _hasStamina;
    protected bool _dodgePressed, _attackPressed, _movePressed, _runPressed;
    protected float _velocityX, _velocityZ;

    protected PlayerBaseState(Player player, PlayerStateMachine stateMachine)
    {
        _player = player;
        _stateMachine = stateMachine;
        _transtions = new List<Transition>();
        InitializeTransitions();
    }

    #region Abstract methods
    public abstract void InitializeTransitions();
    public abstract PlayerStateType GetStateType();
    public abstract void OnEnterState();
    public abstract void ExecuteState();
    #endregion

    public void CheckSwitchState() 
    {
        foreach(Transition transition in _transtions)
        {
            if (transition.Condition())
            {
                if (transition.OnExit != null) transition.OnExit();
                _stateMachine.SwitchState(transition.destination);
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

    #region Decelaration logic
    protected void Decelarate()
    {
        _velocityX = DecelarateAlongAxis(_player.MovementHandler.CurrentMovementVelocity.x);
        _velocityZ = DecelarateAlongAxis(_player.MovementHandler.CurrentMovementVelocity.z);
        _player.MovementHandler.CurrentMovementVelocity = new Vector3(_velocityX, 0, _velocityZ);
    }

    private float DecelarateAlongAxis(float currentVelocity)
    {
        float velocity = currentVelocity;

        if (currentVelocity > _player.MovementHandler.THRESH)
            velocity = currentVelocity - ApplyFrameIndependentDecelaration();
        
        else if (currentVelocity < -_player.MovementHandler.THRESH)
            velocity = currentVelocity + ApplyFrameIndependentDecelaration();
        
        else
            velocity = 0;

        velocity = Mathf.Clamp(velocity, -_player.Config.run.maxVelocity, _player.Config.run.maxVelocity);
        return velocity;
    }

    private float ApplyFrameIndependentDecelaration() => _player.Config.run.decelaration * Time.deltaTime;
    #endregion
}