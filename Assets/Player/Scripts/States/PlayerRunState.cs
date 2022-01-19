using System;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    private bool _hasVelocityX, _hasVelocityZ;

    public PlayerRunState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

    public override void InitializeTransitions()
    {
        _transtions.Add(new Transition(PlayerStateType.walk, ToWalkCondition));
        _transtions.Add(new Transition(PlayerStateType.dodge, ToDodgeCondition));
        _transtions.Add(new Transition(PlayerStateType.melee_running, ToMeleeCondition));
    }

    #region Transition condition
    private bool ToWalkCondition()
    {
        _hasStamina = !_player.StatusHandler.Stamina.IsEmpty();
        _runPressed = _player.InputHandler.IsInputActiveRun;
        return (!_hasStamina || !_runPressed) && !HasRunVelocity();
    }

    private new bool ToMeleeCondition() => base.ToMeleeCondition() && HasReachedMaxVelocity();
    #endregion

    public override Enum GetStateType() => PlayerStateType.run;

    public override void OnEnterState() {}

    #region Velocity checks
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

    public override void ExecuteState() 
    { 
        CheckSwitchState();
        _player.MovementHandler.RotateTowardsCameraDirection();
        
        if (_player.InputHandler.IsInputActiveRun && !_player.StatusHandler.Stamina.IsEmpty())
        {
        _player.MovementHandler.UpdateVelocity(_player.Config.run);
            _player.StatusHandler.DepleteStamina();
        }
        else
            _player.MovementHandler.Decelarate();
    }
}