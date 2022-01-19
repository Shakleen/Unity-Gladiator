using System;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    private bool _hasVelocityX, _hasVelocityZ;

    public PlayerIdleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

    public override void InitializeTransitions()
    {
        _transtions.Add(new Transition(PlayerStateType.dodge, ToDodgeCondition));
        _transtions.Add(new Transition(PlayerStateType.melee_idle, ToMeleeCondition));
        _transtions.Add(new Transition(PlayerStateType.walk, ToWalkCondition));
    }

    private bool ToWalkCondition() => _player.InputHandler.IsInputActiveMovement;

    public override Enum GetStateType() => PlayerStateType.idle;

    public override void OnEnterState() {}

    public override void ExecuteState() 
    { 
        CheckSwitchState(); 
        _player.MovementHandler.Decelarate();
    }
}