using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    protected const float THRESH = 1e-3f;

    public PlayerWalkState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

    public override PlayerStateType GetStateType() { return PlayerStateType.walk; }

    public override void OnEnterState() 
    { 
        _player.AnimatorHandler.SetAnimationValueIsMoving(true);
        _player.AnimatorHandler.SetAnimationValueIsRunning(false);
        _player.AnimatorHandler.SetAnimationValueIsDodging(false);
        _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(false);
    }

    public override void OnExitState() {}

    public override void CheckSwitchState()
    {
        if (GetIsInputActiveDodge())
            _stateMachine.SwitchState(PlayerStateType.dodge);
        else if (GetIsInputActiveMeleeAttack())
            _stateMachine.SwitchState(PlayerStateType.melee_walking);
        else if (!GetIsInputActiveMovement())
        {
            _stateMachine.SwitchState(PlayerStateType.idle);
            _player.AnimatorHandler.SetAnimationValueIsMoving(false);
        }
        else if (GetIsInputActiveRun() && _player.StatusHandler.HasSufficientStamina())
            _stateMachine.SwitchState(PlayerStateType.run);
    }

    private bool GetIsInputActiveRun() { return _player.InputHandler.IsInputActiveRun; }

    private bool GetIsInputActiveMovement() { return _player.InputHandler.IsInputActiveMovement; }

    private bool GetIsInputActiveMeleeAttack() { return _player.InputHandler.IsInputActiveMeleeAttack; }

    private bool GetIsInputActiveDodge() { return _player.InputHandler.IsInputActiveDodge; }

    public override void ExecuteState()
    {
        CheckSwitchState();
        _player.MovementHandler.RotateTowardsCameraDirection();
        UpdateWalkVelocity();
        _player.MovementHandler.MoveCharacter();
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

        velocity = Mathf.Clamp(velocity, -_player.Config.MaxWalkVelocity, _player.Config.MaxWalkVelocity);
        return velocity;
    }

    private float ApplyFrameIndependentAccelaration() { return _player.Config.AccelarationWalk * Time.deltaTime; }

    private float ApplyFrameIndependentDecelaration() { return _player.Config.DecelarationWalk * Time.deltaTime; }
}