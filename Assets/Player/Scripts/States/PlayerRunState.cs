using UnityEngine;

public class PlayerRunState : PlayerBaseMovementState
{
    public PlayerRunState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine, player.Config.run) {}

    public override void InitializeTransitions()
    {
        _transtions.Add(new Transition(PlayerStateType.walk, ToWalkCondition));
        _transtions.Add(new Transition(PlayerStateType.dodge, ToDodgeCondition));
        _transtions.Add(new Transition(PlayerStateType.melee_running, ToMeleeCondition));
    }

    private bool ToWalkCondition()
    {
        bool hasNoStamina = _player.StatusHandler.Stamina.IsEmpty();
        bool runPressed = _player.InputHandler.IsInputActiveRun;
        return (hasNoStamina || !runPressed) && !HasRunVelocity();
    }

    private new bool ToMeleeCondition() => base.ToMeleeCondition() && HasReachedMaxVelocity();

    public override PlayerStateType GetStateType() => PlayerStateType.run;

    public override void OnEnterState() {}

    private bool HasReachedMaxVelocity()
    {
        bool hasRunVelocityX = Mathf.Abs(_player.MovementHandler.CurrentMovementVelocity.x) == _maxMovementVelocity;
        bool hasRunVelocityZ = Mathf.Abs(_player.MovementHandler.CurrentMovementVelocity.z) == _maxMovementVelocity;
        return hasRunVelocityX || hasRunVelocityZ;
    }

    private bool HasRunVelocity()
    {
        bool hasRunVelocityX = Mathf.Abs(_player.MovementHandler.CurrentMovementVelocity.x) > _player.Config.walk.maxVelocity;
        bool hasRunVelocityZ = Mathf.Abs(_player.MovementHandler.CurrentMovementVelocity.z) > _player.Config.walk.maxVelocity;
        return hasRunVelocityX || hasRunVelocityZ;
    }

    public override void ExecuteState() 
    { 
        CheckSwitchState();
        _player.MovementHandler.RotateTowardsCameraDirection();
        
        if (_player.InputHandler.IsInputActiveRun && !_player.StatusHandler.Stamina.IsEmpty())
        {
            UpdateVelocity();
            _player.StatusHandler.DepleteStamina();
        }
        else
            Decelarate();
    }
}