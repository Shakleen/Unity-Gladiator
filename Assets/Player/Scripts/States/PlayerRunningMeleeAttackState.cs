using UnityEngine;

public class PlayerRunningMeleeAttackState : PlayerBaseState
{
    public PlayerRunningMeleeAttackState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

    public override PlayerStateType GetStateType() { return PlayerStateType.melee_running; }

    public override void OnEnterState() 
    { 
        _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(true); 
        _player.StatusHandler.UseStamina(_player.Config.staminaCost.runningMelee);
    }

    public override void OnExitState() { _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(false); }

    public override void CheckSwitchState() 
    {
        if (!_player.AnimatorHandler.IsMeleeAttacking && !_player.InputHandler.IsInputActiveMeleeAttack)
        {
            if (_player.InputHandler.IsInputActiveMovement)
            {
                if (_player.InputHandler.IsInputActiveRun)
                    _stateMachine.SwitchState(PlayerStateType.run);
                else
                    _stateMachine.SwitchState(PlayerStateType.walk);
            }
            else if (!_player.AnimatorHandler.IsAnimationPlaying())
            {
                _player.MovementHandler.StopMovement();
                _stateMachine.SwitchState(PlayerStateType.idle);
            }
        }
    }

    public override void ExecuteState() { CheckSwitchState(); }
}