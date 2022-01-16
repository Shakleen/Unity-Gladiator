using UnityEngine;

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

    public override PlayerStateType GetStateType() => PlayerStateType.melee_walking;

    public override void OnEnterState() 
    { 
        _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(true); 
        _player.StatusHandler.UseStamina(_player.Config.WalkingMeleeAttackStaminaCost);
    }

    private void OnExitState() => _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(false);

    public override void ExecuteState() 
    { 
        CheckSwitchState(); 
        Decelarate();
    }
}