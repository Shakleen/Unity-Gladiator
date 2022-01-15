using UnityEngine;

public class PlayerIdleMeleeAttackState : PlayerBaseState
{
    int _attackNumber;

    public PlayerIdleMeleeAttackState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

    public override PlayerStateType GetStateType() { return PlayerStateType.melee_idle; }

    public override void OnEnterState() 
    { 
        _attackNumber = 1;
        _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(true);
        _player.AnimatorHandler.IncrementMeleeAttackNumber();
        _player.StatusHandler.UseStamina(_player.Config.IdleMeleeAttackStaminaCost);
    }

    public override void OnExitState() 
    { 
        _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(false);
        _player.AnimatorHandler.ResetMeleeAttackNumber();
        _attackNumber = 0;
    }

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

    public override void ExecuteState()
    {
        CheckSwitchState();
        CheckChainCombo();
    }

    private void CheckChainCombo()
    {
        if (_player.AnimatorHandler.IsMeleeAttacking)
        {
            if (_player.InputHandler.IsInputActiveMeleeAttack && IsAttackNumberEqual())
                _attackNumber++;
        }
        else
        {
            if (!IsAttackNumberEqual())
            {
                _player.AnimatorHandler.IncrementMeleeAttackNumber();
                _player.StatusHandler.UseStamina(_player.Config.IdleMeleeAttackStaminaCost);
            }
        }
    }

    private bool IsAttackNumberEqual() { return _attackNumber == _player.AnimatorHandler.MeleeAttackNumber; }
}