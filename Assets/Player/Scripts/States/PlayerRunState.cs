using System;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    private Transition _toDodge, _toAttack, _toWalk, _toDeath;
    private bool _hasVelocityX, _hasVelocityZ;

    public PlayerRunState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) 
    {
        _toDeath = new Transition(GetStateType(), PlayerStateEnum.death);
        _toDodge = new Transition(GetStateType(), PlayerStateEnum.dodge);
        _toAttack = new Transition(GetStateType(), PlayerStateEnum.melee_running);
        _toWalk = new Transition(GetStateType(), PlayerStateEnum.walk);
    }

    public override Enum GetStateType() => PlayerStateEnum.run;

    public override void OnEnterState(Transition transition) {}

    public override void ExecuteState() 
    { 
        CheckSwitchState();
        _player.MovementHandler.RotateTowardsCameraDirection();
        
        if (_player.InputHandler.IsInputActiveRun && hasStamina())
        {
            _player.MovementHandler.UpdateVelocity(_player.Config.run);
            _player.StatusHandler.DepleteStamina();
        }
        else
            _player.MovementHandler.Decelarate();
    }

    #region Switch state conditions
    public override Transition GetTransition()
    {
        if (IsDeath())
            return _toDeath;
        if (hasStamina())
        {
            if (isDodgePressed())
                return _toDodge;
            if (isAttackPressed() && HasReachedMaxVelocity())
                return _toAttack;
        }
        
        if ((!hasStamina() || !isRunPressed()) && !HasRunVelocity())
            return _toWalk;

        return null;
    }

    private bool HasReachedMaxVelocity()
    {
        _hasVelocityX = Mathf.Abs(_player.MovementHandler.CurrentMovementVelocity.x) == _player.Config.run.maxVelocity;
        _hasVelocityZ = Mathf.Abs(_player.MovementHandler.CurrentMovementVelocity.z) == _player.Config.run.maxVelocity;
        return _hasVelocityX || _hasVelocityZ;
    }

    private bool HasRunVelocity()
    {
        _hasVelocityX = Mathf.Abs(_player.MovementHandler.CurrentMovementVelocity.x) > _player.Config.walk.maxVelocity;
        _hasVelocityZ = Mathf.Abs(_player.MovementHandler.CurrentMovementVelocity.z) > _player.Config.walk.maxVelocity;
        return _hasVelocityX || _hasVelocityZ;
    }
    #endregion

    public override void OnExitState(Transition transition) {}
}