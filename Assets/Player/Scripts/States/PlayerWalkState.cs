public class PlayerWalkState : PlayerBaseMovementState
{
    protected const float THRESH = 1e-3f;

    public PlayerWalkState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine, player.Config.walk) {}

    public override void InitializeTransitions()
    {
        _transtions.Add(new Transition(PlayerStateType.dodge, ToDodgeCondition));
        _transtions.Add(new Transition(PlayerStateType.melee_walking, ToMeleeCondition));
        _transtions.Add(new Transition(PlayerStateType.run, ToRunCondition));
        _transtions.Add(new Transition(PlayerStateType.idle, ToIdleCondition));
    }

    private bool ToIdleCondition() => !_player.InputHandler.IsInputActiveMovement;

    public override PlayerStateType GetStateType() => PlayerStateType.walk;

    public override void OnEnterState() => _player.AnimatorHandler.SetAnimationValueIsDodging(false);

    public override void ExecuteState()
    {
        CheckSwitchState();
        _player.MovementHandler.RotateTowardsCameraDirection();
        UpdateVelocity();
    }
}