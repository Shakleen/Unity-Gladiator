using System;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

    public override void InitializeTransitions()
    {
        _transtions.Add(new Transition(PlayerStateType.walk, ToWalkCondition, () => _player.AnimatorHandler.SetAnimationValueIsRunning(false)));
        _transtions.Add(new Transition(PlayerStateType.dodge, ToDodgeCondition));
        _transtions.Add(new Transition(PlayerStateType.melee_running, ToMeleeCondition));
    }

    private bool ToWalkCondition()
    {
        bool hasNoStamina = _player.StatusHandler.Stamina.IsEmpty();
        bool runPressed = _player.InputHandler.IsInputActiveRun;
        return hasNoStamina || (!runPressed && !HasRunVelocity());
    }

    private new bool ToMeleeCondition() => base.ToMeleeCondition() && HasReachedMaxVelocity();

    public override PlayerStateType GetStateType() => PlayerStateType.run;

    public override void OnEnterState() { _player.AnimatorHandler.SetAnimationValueIsRunning(true); }

    private bool HasReachedMaxVelocity()
    {
        bool hasRunVelocityX = Mathf.Abs(_player.MovementHandler.CurrentMovementVelocityX) == _player.Config.run.maxVelocity;
        bool hasRunVelocityZ = Mathf.Abs(_player.MovementHandler.CurrentMovementVelocityZ) == _player.Config.run.maxVelocity;
        return hasRunVelocityX || hasRunVelocityZ;
    }

    private bool HasRunVelocity()
    {
        bool hasRunVelocityX = Mathf.Abs(_player.MovementHandler.CurrentMovementVelocityX) > _player.Config.walk.maxVelocity;
        bool hasRunVelocityZ = Mathf.Abs(_player.MovementHandler.CurrentMovementVelocityZ) > _player.Config.walk.maxVelocity;
        return hasRunVelocityX || hasRunVelocityZ;
    }

    public override void ExecuteState() 
    { 
        CheckSwitchState();
        _player.MovementHandler.RotateTowardsCameraDirection();
        UpdateRunVelocity();
        _player.StatusHandler.DepleteStamina();
    }

    private void UpdateRunVelocity()
    {
        Vector2 inputMoveVector = _player.InputHandler.InputMoveVector;

        float velocityX = _player.MovementHandler.CurrentMovementVelocityX;
        velocityX = ChangeAxisVelocity(inputMoveVector.x, velocityX);
        _player.MovementHandler.CurrentMovementVelocityX = velocityX;

        float velocityZ = _player.MovementHandler.CurrentMovementVelocityZ;
        velocityZ = ChangeAxisVelocity(inputMoveVector.y, velocityZ);
        _player.MovementHandler.CurrentMovementVelocityZ = velocityZ;
    }

    private float ChangeAxisVelocity(float inputVelocity, float currentVelocity)
    {
        float velocity = currentVelocity;

        // Player pressed button to move in the positive direction of axis
        if (inputVelocity > 0 && _player.InputHandler.IsInputActiveRun)
            velocity = currentVelocity + ApplyFrameIndependentAccelaration();
        
        // Player pressed button to move in the negative direction of axis
        else if (inputVelocity < 0 && _player.InputHandler.IsInputActiveRun)
            velocity = currentVelocity - ApplyFrameIndependentAccelaration();
        
        // Player hasn't pressed any button for this axis but was moving in the positive direction.
        else if (currentVelocity > _player.MovementHandler.THRESH)
            velocity = currentVelocity - ApplyFrameIndependentDecelaration();
        
        // Player hasn't pressed any button for this axis but was moving in the negative direction.
        else if (currentVelocity < -_player.MovementHandler.THRESH)
            velocity = currentVelocity + ApplyFrameIndependentDecelaration();
        
        else
            velocity = 0;

        velocity = Mathf.Clamp(velocity, -_player.Config.run.maxVelocity, _player.Config.run.maxVelocity);
        return velocity;
    }

    private float ApplyFrameIndependentAccelaration() { return _player.Config.run.accelaration * Time.deltaTime; }

    private new float ApplyFrameIndependentDecelaration() { return _player.Config.run.decelaration * Time.deltaTime; }
}