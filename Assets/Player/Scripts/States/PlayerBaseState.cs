using System;
using System.Collections.Generic;

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

    protected bool AttackNotPressedAndNotAttacking() 
    {
        bool attackPressed = _player.InputHandler.IsInputActiveMeleeAttack;
        bool isAttacking = _player.AnimatorHandler.IsMeleeAttacking;
        return !attackPressed && !isAttacking;
    }

    protected bool AttackToDodgeCondition() => AttackNotPressedAndNotAttacking() && ToDodgeCondition();

    protected bool AttackToRunCondition() => AttackNotPressedAndNotAttacking() && ToRunCondition();

    protected bool AttackToWalkCondition()
    {
        bool movePressed = _player.InputHandler.IsInputActiveMovement;
        bool runPressed = _player.InputHandler.IsInputActiveRun;
        return AttackNotPressedAndNotAttacking() && movePressed && !runPressed;
    }

    protected bool AttackToIdleCondition()
    {
        bool movePressed = _player.InputHandler.IsInputActiveMovement;
        bool runPressed = _player.InputHandler.IsInputActiveRun;
        bool animationDone = !_player.AnimatorHandler.IsAnimationPlaying();
        return AttackNotPressedAndNotAttacking() && !movePressed && !runPressed && animationDone;
    }
}