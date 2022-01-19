using System;

public class PlayerIdleMeleeAttackState : PlayerBaseState
{
    private Transition _toDodge, _toWalk, _toIdle, _toDeath;
    
    public PlayerIdleMeleeAttackState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
        _toDeath = new Transition(GetStateType(), PlayerStateEnum.death);
        _toDodge = new Transition(GetStateType(), PlayerStateEnum.dodge);
        _toWalk = new Transition(GetStateType(), PlayerStateEnum.walk);
        _toIdle = new Transition(GetStateType(), PlayerStateEnum.idle);
    }

    public override Enum GetStateType() => PlayerStateEnum.melee_idle;

    public override void OnEnterState(Transition transition) {}

    public override void ExecuteState()
    {
        CheckSwitchState();
        _player.MovementHandler.Decelarate();
        _player.AttackHandler.Attack();
    }

    public override Transition GetTransition()
    {
        if (IsDeath())
            return _toDeath;
        if (_player.AttackHandler.NoAttackActivity())
        {
            if (hasStamina() && isDodgePressed())
                return _toDodge;
            else if (isMovePressed())
                return _toWalk;
            else if (_player.AttackHandler.IsAttackComplete())
                return _toIdle;
        }

        return null;
    }

    public override void OnExitState(Transition transition)
    {
        if (IsEqual(transition, PlayerStateEnum.run) || IsEqual(transition, PlayerStateEnum.dodge))
            _player.AttackHandler.ResetCombo();

        _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(false);
        _player.AttackHandler.SetWeaponDamageMode(false);
    }

    private bool IsEqual(Transition transition, PlayerStateEnum state) => state.CompareTo(transition.destination) == 0;
}