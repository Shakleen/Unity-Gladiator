using System;

public class PlayerWalkingMeleeAttackState : PlayerBaseMeleeAttackState
{
    public PlayerWalkingMeleeAttackState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

    public override void InitializeTransitions()
    {
        _transtions.Add(new Transition(PlayerStateType.dodge, AttackToDodgeCondition, OnExitState));
        _transtions.Add(new Transition(PlayerStateType.run, AttackToRunCondition, OnExitState));
        _transtions.Add(new Transition(PlayerStateType.walk, AttackToWalkCondition, OnExitState));
        _transtions.Add(new Transition(PlayerStateType.idle, AttackToIdleCondition, OnExitState));
    }

    public override Enum GetStateType() => PlayerStateType.melee_walking;

    public override void OnEnterState() {}

    private void OnExitState() 
    {
        _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(false);
        _player.AttackHandler.SetWeaponDamageMode(false);
    }

    public override void ExecuteState() 
    { 
        CheckSwitchState(); 
        Decelarate();
        _player.AttackHandler.ChargeAttack();
    }
}