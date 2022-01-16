using UnityEngine;

public class PlayerRunningMeleeAttackState : PlayerBaseMeleeAttackState
{
    public PlayerRunningMeleeAttackState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

    public override void InitializeTransitions()
    {
        _transtions.Add(new Transition(PlayerStateType.run, AttackToRunCondition, OnExitState));
        _transtions.Add(new Transition(PlayerStateType.walk, AttackToWalkCondition, OnExitState));
        _transtions.Add(new Transition(PlayerStateType.idle, AttackToIdleCondition, OnExitState));
    }

    public override PlayerStateType GetStateType() => PlayerStateType.melee_running;

    public override void OnEnterState() 
    { 
        _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(true); 
        _player.StatusHandler.UseStamina(_player.Config.staminaCost.runningMeleeAttack);
    }

    private void OnExitState() 
    {
        _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(false);
        _player.MovementHandler.StopMovement();
    }

    public override void ExecuteState() 
    { 
        CheckSwitchState(); 
        Decelarate();
    }
}