using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

    public override PlayerStateType GetStateType() { return PlayerStateType.idle; }

    public override void OnEnterState() 
    { 
        _player.AnimatorHandler.SetAnimationValueIsMoving(false);
        _player.AnimatorHandler.SetAnimationValueIsRunning(false);
        _player.AnimatorHandler.SetAnimationValueIsDodging(false);
        _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(false);
        _player.AnimatorHandler.ResetMeleeAttackNumber();
    }

    public override void OnExitState() {}

    public override void CheckSwitchState() 
    {
        if (_player.InputHandler.IsInputActiveDodge)
            _stateMachine.SwitchState(PlayerStateType.dodge);
        else if (_player.InputHandler.IsInputActiveMeleeAttack)
            _stateMachine.SwitchState(PlayerStateType.melee_idle);
        else if (_player.InputHandler.IsInputActiveMovement)
            _stateMachine.SwitchState(PlayerStateType.walk);
    }

    public override void ExecuteState() 
    { 
        CheckSwitchState(); 
        
        if (HasMovementVelocity())
        {
            Decelarate();
            _player.MovementHandler.MoveCharacter();
        }
    }

    private bool HasMovementVelocity()
    {
        bool hasVelocityX = Mathf.Abs(_player.MovementHandler.CurrentMovementVelocityX) > _player.MovementHandler.THRESH;
        bool hasVelocityZ = Mathf.Abs(_player.MovementHandler.CurrentMovementVelocityZ) > _player.MovementHandler.THRESH;
        return hasVelocityX || hasVelocityZ;
    }

    private void Decelarate()
    {
        float velocityX = DecelarateAlongAxis(_player.MovementHandler.CurrentMovementVelocityX);
        _player.MovementHandler.CurrentMovementVelocityX = velocityX;

        float velocityZ = DecelarateAlongAxis(_player.MovementHandler.CurrentMovementVelocityZ);
        _player.MovementHandler.CurrentMovementVelocityZ = velocityZ;
    }

    private float DecelarateAlongAxis(float currentVelocity)
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

    private float ApplyFrameIndependentDecelaration() { return _player.Config.DecelarationRun * Time.deltaTime; }
}