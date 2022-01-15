using UnityEngine;

public class PlayerWalkingMeleeAttackState : PlayerBaseState
{
    public PlayerWalkingMeleeAttackState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

    public override PlayerStateType GetStateType() { return PlayerStateType.melee_walking; }

    public override void OnEnterState() 
    { 
        _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(true); 
        _player.StatusHandler.UseStamina(_player.Config.WalkingMeleeAttackStaminaCost);
    }

    public override void OnExitState() { _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(false); }

    public override void CheckSwitchState() 
    {
        if (!_player.AnimatorHandler.IsMeleeAttacking && !_player.InputHandler.IsInputActiveMeleeAttack)
        {
            if (_player.InputHandler.IsInputActiveDodge)
                _stateMachine.SwitchState(PlayerStateType.dodge);
            else if (_player.InputHandler.IsInputActiveMovement)
            {
                if (_player.InputHandler.IsInputActiveRun)
                    _stateMachine.SwitchState(PlayerStateType.run);
                else
                    _stateMachine.SwitchState(PlayerStateType.walk);
            }
            else if (!_player.AnimatorHandler.IsAnimationPlaying())
                _stateMachine.SwitchState(PlayerStateType.idle);
        }
    }

    public override void ExecuteState() { CheckSwitchState(); }
}