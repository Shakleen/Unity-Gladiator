using System;

public class PlayerDeathState : PlayerBaseState
{
    public PlayerDeathState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

    public override Enum GetStateType() => PlayerStateEnum.idle;

    public override void OnEnterState(Transition transition) {}

    public override void ExecuteState() => _player.StatusHandler.Die();
    
    public override Transition GetTransition() => null;

    public override void OnExitState(Transition transition) {}
}