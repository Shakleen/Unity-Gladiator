using System;

public class PlayerIdleMeleeAttackState : PlayerBaseMeleeAttackState
{
    public PlayerIdleMeleeAttackState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

    public override void InitializeTransitions()
    {
        _transtions.Add(new Transition(PlayerStateType.dodge, AttackToDodgeCondition, OnExitState));
        _transtions.Add(new Transition(PlayerStateType.run, AttackToRunCondition, OnExitState));
        _transtions.Add(new Transition(PlayerStateType.walk, AttackToWalkCondition, OnExitState));
        _transtions.Add(new Transition(PlayerStateType.idle, AttackToIdleCondition));
    }

    public override Enum GetStateType() => PlayerStateType.melee_idle;

    public override void OnEnterState() {}

    private void OnExitState() 
    {
        _player.AttackHandler.ResetCombo();
        _player.AttackHandler.SetWeaponDamageMode(false);
    }

    public override void ExecuteState()
    {
        CheckSwitchState();
        Decelarate();
        _player.AttackHandler.Attack();
    }
}