using System;

public class PlayerRunningMeleeAttackState : PlayerBaseState
{
    private Transition _toIdle, _toDeath;

    public PlayerRunningMeleeAttackState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
        _toDeath = new Transition(GetStateType(), PlayerStateEnum.death);
        _toIdle = new Transition(GetStateType(), PlayerStateEnum.idle);
    }

    public override Enum GetStateType() => PlayerStateEnum.melee_running;

    public override void OnEnterState(Transition transition) {}

    public override void ExecuteState() 
    { 
        CheckSwitchState();
        _player.MovementHandler.Decelarate();
        _player.AttackHandler.ChargeAttack();
    }

    public override Transition GetTransition()
    {
        if (IsDeath())
            return _toDeath;
        if (_player.AttackHandler.NoAttackActivity())
            return _toIdle;
        
        return null;
    }

    public override void OnExitState(Transition transition) 
    {
        _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(false);
        _player.AttackHandler.SetWeaponDamageMode(false);
    }
}