using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

    public override PlayerStateType GetStateType() { return PlayerStateType.run; }

    public override void OnEnterState() { _player.AnimatorHandler.SetAnimationValueIsRunning(true); }

    public override void OnExitState() {}

    public override void CheckSwitchState()
    {
        if (_player.InputHandler.IsInputActiveDodge)
            _stateMachine.SwitchState(PlayerStateType.dodge);
        else if (_player.InputHandler.IsInputActiveMeleeAttack && HasReachedMaxVelocity())
            _stateMachine.SwitchState(PlayerStateType.melee_running);
        else if (!_player.InputHandler.IsInputActiveRun && !HasRunVelocity())
        {
            _player.AnimatorHandler.SetAnimationValueIsRunning(false);
            _stateMachine.SwitchState(PlayerStateType.walk);
        }
    }

    private bool HasReachedMaxVelocity()
    {
        bool hasRunVelocityX = Mathf.Abs(_player.MovementHandler.CurrentMovementVelocityX) == _player.Config.MaxVelocityRun;
        bool hasRunVelocityZ = Mathf.Abs(_player.MovementHandler.CurrentMovementVelocityZ) == _player.Config.MaxVelocityRun;
        return hasRunVelocityX || hasRunVelocityZ;
    }

    private bool HasRunVelocity()
    {
        bool hasRunVelocityX = Mathf.Abs(_player.MovementHandler.CurrentMovementVelocityX) > _player.Config.MaxWalkVelocity;
        bool hasRunVelocityZ = Mathf.Abs(_player.MovementHandler.CurrentMovementVelocityZ) > _player.Config.MaxWalkVelocity;
        return hasRunVelocityX || hasRunVelocityZ;
    }

    public override void ExecuteState() 
    { 
        CheckSwitchState();
        _player.MovementHandler.RotateTowardsCameraDirection();
        UpdateRunVelocity();
        _player.MovementHandler.MoveCharacter();
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

        velocity = Mathf.Clamp(velocity, -_player.Config.MaxVelocityRun, _player.Config.MaxVelocityRun);
        return velocity;
    }

    private float ApplyFrameIndependentAccelaration() { return _player.Config.AccelarationRun * Time.deltaTime; }

    private float ApplyFrameIndependentDecelaration() { return _player.Config.DecelarationRun * Time.deltaTime; }
}