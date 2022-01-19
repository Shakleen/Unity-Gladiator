using System;

public class PlayerIdleMeleeAttackState : PlayerBaseState
{
    private Transition _toDodge, _toRun, _toWalk, _toIdle;
    
    public PlayerIdleMeleeAttackState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
        _toDodge = new Transition(GetStateType(), PlayerStateEnum.dodge);
        _toRun = new Transition(GetStateType(), PlayerStateEnum.run);
        _toWalk = new Transition(GetStateType(), PlayerStateEnum.walk);
        _toIdle = new Transition(GetStateType(), PlayerStateEnum.idle);
    }

    public override Enum GetStateType() => PlayerStateEnum.melee_idle;

    public override void OnEnterState(Transition transition) {}

    public override void ExecuteState()
    {
        CheckSwitchState();
        _player.MovementHandler.Decelarate();
        _player.AttackHandler.Attack();
    }

    public override Transition GetTransition()
    {
        if (_player.AttackHandler.NoAttackActivity())
        {
            if (hasStamina())
            {
                if (isDodgePressed())
                    return _toDodge;
                else if (isRunPressed() && isMovePressed())
                    return _toRun;
            }

            if (isMovePressed())
                return _toWalk;
            else
                return _toIdle;
        }

        return null;
    }

    public override void OnExitState(Transition transition)
    {
        switch((PlayerStateEnum) transition.destination)
        {
            case PlayerStateEnum.idle:
                break;
            case PlayerStateEnum.walk:
            case PlayerStateEnum.run:
                _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(false);
                _player.AttackHandler.SetWeaponDamageMode(false);
                break;
            default:
                _player.AttackHandler.ResetCombo();
                _player.AttackHandler.SetWeaponDamageMode(false);
                break;
        }
    }
}