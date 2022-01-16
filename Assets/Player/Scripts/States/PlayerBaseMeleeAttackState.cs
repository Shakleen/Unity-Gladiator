using UnityEngine;

public abstract class PlayerBaseMeleeAttackState : PlayerBaseState
{
    protected PlayerBaseMeleeAttackState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

    protected bool AttackNotPressedAndNotAttacking() 
    {
        bool attackPressed = _player.InputHandler.IsInputActiveMeleeAttack;
        bool isAttacking = _player.AnimatorHandler.IsMeleeAttacking;
        return !attackPressed && !isAttacking;
    }

    protected bool AttackToDodgeCondition() => AttackNotPressedAndNotAttacking() && ToDodgeCondition();

    protected bool AttackToRunCondition() => AttackNotPressedAndNotAttacking() && ToRunCondition();

    protected bool AttackToWalkCondition()
    {
        bool movePressed = _player.InputHandler.IsInputActiveMovement;
        bool runPressed = _player.InputHandler.IsInputActiveRun;
        return AttackNotPressedAndNotAttacking() && movePressed && !runPressed;
    }

    protected bool AttackToIdleCondition()
    {
        bool movePressed = _player.InputHandler.IsInputActiveMovement;
        bool runPressed = _player.InputHandler.IsInputActiveRun;
        bool animationDone = !_player.AnimatorHandler.IsAnimationPlaying();
        return AttackNotPressedAndNotAttacking() && !movePressed && !runPressed && animationDone;
    }
}