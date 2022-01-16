using System;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    protected const float THRESH = 1e-3f;

    public PlayerWalkState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

    public override void InitializeTransitions()
    {
        _transtions.Add(new Transition(PlayerStateType.dodge, ToDodgeCondition));
        _transtions.Add(new Transition(PlayerStateType.melee_walking, ToMeleeCondition));
        _transtions.Add(new Transition(PlayerStateType.run, ToRunCondition));
        _transtions.Add(new Transition(PlayerStateType.idle, ToIdleCondition, () => _player.AnimatorHandler.SetAnimationValueIsMoving(false)));
    }

    private bool ToIdleCondition()
    {
        bool movePressed = _player.InputHandler.IsInputActiveMovement; 
        return !movePressed;
    }

    public override PlayerStateType GetStateType() => PlayerStateType.walk;

    public override void OnEnterState() 
    { 
        _player.AnimatorHandler.SetAnimationValueIsMoving(true);
        _player.AnimatorHandler.SetAnimationValueIsRunning(false);
        _player.AnimatorHandler.SetAnimationValueIsDodging(false);
        _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(false);
    }

    public override void ExecuteState()
    {
        CheckSwitchState();
        _player.MovementHandler.RotateTowardsCameraDirection();
        UpdateWalkVelocity();
        // _player.MovementHandler.MoveCharacter();
    }

    private void UpdateWalkVelocity()
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
        if (inputVelocity > 0)
            velocity = currentVelocity + ApplyFrameIndependentAccelaration();
        
        // Player pressed button to move in the negative direction of axis
        else if (inputVelocity < 0)
            velocity = currentVelocity - ApplyFrameIndependentAccelaration();
        
        // Player hasn't pressed any button for this axis but was moving in the positive direction.
        else if (currentVelocity > THRESH)
            velocity = currentVelocity - ApplyFrameIndependentDecelaration();
        
        // Player hasn't pressed any button for this axis but was moving in the negative direction.
        else if (currentVelocity < -THRESH)
            velocity = currentVelocity + ApplyFrameIndependentDecelaration();
        
        else
            velocity = 0;

        velocity = Mathf.Clamp(velocity, -_player.Config.walk.maxVelocity, _player.Config.walk.maxVelocity);
        return velocity;
    }

    private float ApplyFrameIndependentAccelaration() { return _player.Config.walk.accelaration * Time.deltaTime; }

    private new float ApplyFrameIndependentDecelaration() { return _player.Config.walk.decelaration * Time.deltaTime; }
}