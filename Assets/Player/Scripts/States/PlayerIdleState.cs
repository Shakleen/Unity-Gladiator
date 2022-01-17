using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

    public override void InitializeTransitions()
    {
        _transtions.Add(new Transition(PlayerStateType.dodge, ToDodgeCondition));
        _transtions.Add(new Transition(PlayerStateType.melee_idle, ToMeleeCondition));
        _transtions.Add(new Transition(PlayerStateType.walk, ToWalkCondition));
    }

    private bool ToWalkCondition()
    {
        bool walkPressed = _player.InputHandler.IsInputActiveMovement;
        return walkPressed;
    }

    public override PlayerStateType GetStateType() => PlayerStateType.idle;

    public override void OnEnterState() 
    { 
        _player.AnimatorHandler.SetAnimationValueIsMoving(false);
        _player.AnimatorHandler.SetAnimationValueIsRunning(false);
        _player.AnimatorHandler.SetAnimationValueIsDodging(false);
        _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(false);
        _player.AnimatorHandler.ResetMeleeAttackNumber();
    }

    public override void ExecuteState() 
    { 
        CheckSwitchState(); 
        
        if (HasMovementVelocity())
            Decelarate();
        else
            _player.MovementHandler.StopMovement();
    }

    private bool HasMovementVelocity()
    {
        bool hasVelocityX = Mathf.Abs(_player.MovementHandler.CurrentMovementVelocity.x) > _player.MovementHandler.THRESH;
        bool hasVelocityZ = Mathf.Abs(_player.MovementHandler.CurrentMovementVelocity.z) > _player.MovementHandler.THRESH;
        return hasVelocityX || hasVelocityZ;
    }
}