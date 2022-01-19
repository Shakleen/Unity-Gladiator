using System;

public class PlayerWalkState : PlayerBaseState
{
    private Transition _toDodge, _toAttack, _toRun, _toIdle, _toDeath;

    public PlayerWalkState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
        _toDeath = new Transition(GetStateType(), PlayerStateEnum.death);
        _toDodge = new Transition(GetStateType(), PlayerStateEnum.dodge);
        _toAttack = new Transition(GetStateType(), PlayerStateEnum.melee_walking);
        _toRun = new Transition(GetStateType(), PlayerStateEnum.run);
        _toIdle = new Transition(GetStateType(), PlayerStateEnum.idle);
    }

    private bool ToIdleCondition() => !_player.InputHandler.IsInputActiveMovement;

    public override void OnEnterState(Transition transition) {}

    public override Enum GetStateType() => PlayerStateEnum.walk;

    public override void ExecuteState()
    {
        CheckSwitchState();
        _player.MovementHandler.RotateTowardsCameraDirection();
        _player.MovementHandler.UpdateVelocity(_player.Config.walk);
    }

    public override Transition GetTransition()
    {
        if (IsDeath())
            return _toDeath;
        if (hasStamina())
        {
            if (isDodgePressed())
                return _toDodge;
            else if (isAttackPressed())
                return _toAttack;
            else if (isRunPressed() && isMovePressed())
                return _toRun;
        }
        
        if (!isMovePressed())
            return _toIdle;

        return null;
    }

    public override void OnExitState(Transition transition) {}
}