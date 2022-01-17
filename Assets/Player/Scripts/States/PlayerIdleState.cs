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

    public override PlayerStateType GetStateType() => PlayerStateType.idle;

    public override void OnEnterState() {}

    public override void ExecuteState() 
    { 
        CheckSwitchState(); 
        
        if (HasMovementVelocity())
            Decelarate();
        else
            _player.MovementHandler.StopMovement();
    }

    private bool HasMovementVelocity()
    {
        _hasVelocityX = Mathf.Abs(_player.MovementHandler.CurrentMovementVelocity.x) > _player.MovementHandler.THRESH;
        _hasVelocityZ = Mathf.Abs(_player.MovementHandler.CurrentMovementVelocity.z) > _player.MovementHandler.THRESH;
        return _hasVelocityX || _hasVelocityZ;
    }
}