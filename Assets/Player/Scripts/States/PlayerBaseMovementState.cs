using UnityEngine;

public abstract class PlayerBaseMovementState : PlayerBaseState
{
    protected float _minMovementValue;
    protected float _maxMovementVelocity;
    protected float _accelaration;
    protected float _decelaration;
    protected float _thresh;

    protected PlayerBaseMovementState(
        Player player, 
        PlayerStateMachine stateMachine,
        MoveConfig config
    ) : base(player, stateMachine) 
    {
        this._minMovementValue = -config.maxVelocity;
        this._maxMovementVelocity = config.maxVelocity;
        this._accelaration = config.accelaration;
        this._decelaration = config.decelaration;
        this._thresh = _player.MovementHandler.THRESH;
    }

    protected void UpdateVelocity()
    {
        float velocityX = ChangeAxisVelocity(_player.InputHandler.InputMoveVector.x, _player.MovementHandler.CurrentMovementVelocity.x);
        float velocityZ = ChangeAxisVelocity(_player.InputHandler.InputMoveVector.y, _player.MovementHandler.CurrentMovementVelocity.z);
        _player.MovementHandler.CurrentMovementVelocity = new Vector3(velocityX, 0, velocityZ);
    }

    private float ChangeAxisVelocity(float inputVelocity, float currentVelocity)
    {
        float velocity = currentVelocity;

        // Player pressed button to move in the positive direction of axis
        if (inputVelocity > 0)
            velocity = currentVelocity + MakeFrameIndependent(_accelaration);
        
        // Player pressed button to move in the negative direction of axis
        else if (inputVelocity < 0)
            velocity = currentVelocity - MakeFrameIndependent(_accelaration);
        
        // Player hasn't pressed any button for this axis but was moving in the positive direction.
        else if (currentVelocity > _thresh)
            velocity = currentVelocity - MakeFrameIndependent(_decelaration);
        
        // Player hasn't pressed any button for this axis but was moving in the negative direction.
        else if (currentVelocity < -_thresh)
            velocity = currentVelocity + MakeFrameIndependent(_decelaration);
        
        else
            velocity = 0;

        velocity = Mathf.Clamp(velocity, _minMovementValue, _maxMovementVelocity);
        return velocity;
    }

    private float MakeFrameIndependent(float value) => value * Time.deltaTime;


}