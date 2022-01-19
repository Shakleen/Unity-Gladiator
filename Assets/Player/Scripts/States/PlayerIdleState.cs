using System;

public class PlayerIdleState : PlayerBaseState
{
    private Transition _toDodge, _toAttack, _toWalk, _toDeath;

    public PlayerIdleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) 
    {
        _toDodge = new Transition(GetStateType(), PlayerStateEnum.dodge);
        _toAttack = new Transition(GetStateType(), PlayerStateEnum.melee_idle);
        _toWalk = new Transition(GetStateType(), PlayerStateEnum.walk);
        _toDeath = new Transition(GetStateType(), PlayerStateEnum.death);
    }

    public override Enum GetStateType() => PlayerStateEnum.idle;

    public override void OnEnterState(Transition transition) {}

    public override void ExecuteState() 
    { 
        CheckSwitchState(); 
        _player.MovementHandler.Decelarate();
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
        }
        
        if (isMovePressed())
            return _toWalk;

        return null;
    }

    public override void OnExitState(Transition transition) {}
}