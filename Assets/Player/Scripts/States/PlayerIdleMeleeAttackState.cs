using UnityEngine;

public class PlayerIdleMeleeAttackState : PlayerBaseMeleeAttackState
{
    int _attackNumber;

    public PlayerIdleMeleeAttackState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

    public override void InitializeTransitions()
    {
        _transtions.Add(new Transition(PlayerStateType.dodge, AttackToDodgeCondition, OnExitState));
        _transtions.Add(new Transition(PlayerStateType.run, AttackToRunCondition, OnExitState));
        _transtions.Add(new Transition(PlayerStateType.walk, AttackToWalkCondition, OnExitState));
        _transtions.Add(new Transition(PlayerStateType.idle, AttackToIdleCondition, OnExitState));
    }

    public override PlayerStateType GetStateType() => PlayerStateType.melee_idle;

    public override void OnEnterState() 
    { 
        _attackNumber = 1;
        _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(true);
        _player.AnimatorHandler.IncrementMeleeAttackNumber();
        _player.StatusHandler.UseStamina(_player.Config.IdleMeleeAttackStaminaCost);
    }

    private void OnExitState() 
    { 
        _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(false);
        _player.AnimatorHandler.ResetMeleeAttackNumber();
        _attackNumber = 0;
    }

    public override void ExecuteState()
    {
        CheckSwitchState();
        Decelarate();
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