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

    protected PlayerBaseState(Player player, PlayerStateMachine stateMachine)
    {
        _player = player;
        _stateMachine = stateMachine;
        _transtions = new List<Transition>();
        InitializeTransitions();
    }

    public abstract void InitializeTransitions();

    public abstract PlayerStateType GetStateType();

    public abstract void OnEnterState();

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

    public abstract void ExecuteState();

    protected bool ToDodgeCondition()
    {
        bool hasStamina = !_player.StatusHandler.Stamina.IsEmpty();
        bool dodgePressed = _player.InputHandler.IsInputActiveDodge;
        return hasStamina && dodgePressed;
    }

    protected bool ToMeleeCondition()
    {
        bool hasStamina = !_player.StatusHandler.Stamina.IsEmpty();
        bool meleePressed = _player.InputHandler.IsInputActiveMeleeAttack;
        return hasStamina && meleePressed;
    }

    protected bool ToRunCondition()
    {
        bool hasStamina = !_player.StatusHandler.Stamina.IsEmpty();
        bool movePressed = _player.InputHandler.IsInputActiveMovement; 
        bool runPressed = _player.InputHandler.IsInputActiveRun;
        return hasStamina && movePressed && runPressed;
    }

    protected void Decelarate()
    {
        float velocityX = DecelarateAlongAxis(_player.MovementHandler.CurrentMovementVelocityX);
        _player.MovementHandler.CurrentMovementVelocityX = velocityX;

        float velocityZ = DecelarateAlongAxis(_player.MovementHandler.CurrentMovementVelocityZ);
        _player.MovementHandler.CurrentMovementVelocityZ = velocityZ;
    }

    protected float DecelarateAlongAxis(float currentVelocity)
    {
        float velocity = currentVelocity;

        if (currentVelocity > _player.MovementHandler.THRESH)
            velocity = currentVelocity - ApplyFrameIndependentDecelaration();
        
        // Player hasn't pressed any button for this axis but was moving in the negative direction.
        else if (currentVelocity < -_player.MovementHandler.THRESH)
            velocity = currentVelocity + ApplyFrameIndependentDecelaration();
        
        else
            velocity = 0;

        velocity = Mathf.Clamp(velocity, -_player.Config.MaxVelocityRun, _player.Config.MaxVelocityRun);
        return velocity;
    }

    protected float ApplyFrameIndependentDecelaration() { return _player.Config.DecelarationRun * Time.deltaTime; }
}