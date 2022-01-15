using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

    public override PlayerStateType GetStateType() { return PlayerStateType.run; }

    public override void OnEnterState() { _player.AnimatorHandler.SetAnimationValueIsRunning(true); }

    public override void OnExitState() {}

    public override void CheckSwitchState()
    {
        if (HasNoStamina() || DoesNotWantToRun())
        {
            _player.AnimatorHandler.SetAnimationValueIsRunning(false);
            _stateMachine.SwitchState(PlayerStateType.walk);
        }
        else
        {
            if (WantsToDodge())
                _stateMachine.SwitchState(PlayerStateType.dodge);
            else if (WantsToPerformRunningMelee())
                _stateMachine.SwitchState(PlayerStateType.melee_running);
        }
    }

    private bool HasNoStamina() { return _player.StatusHandler.Stamina.IsEmpty(); }

    private bool DoesNotWantToRun() { return (!_player.InputHandler.IsInputActiveRun && !HasRunVelocity()); }

    private bool WantsToDodge() { return _player.InputHandler.IsInputActiveDodge; }

    private bool WantsToPerformRunningMelee() { return _player.InputHandler.IsInputActiveMeleeAttack && HasReachedMaxVelocity(); }

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

        velocity = Mathf.Clamp(velocity, -_player.Config.run.maxVelocity, _player.Config.run.maxVelocity);
        return velocity;
    }

    private float ApplyFrameIndependentAccelaration() { return _player.Config.run.accelaration * Time.deltaTime; }

    private float ApplyFrameIndependentDecelaration() { return _player.Config.run.decelaration * Time.deltaTime; }
}