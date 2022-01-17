using UnityEngine;

public abstract class PlayerBaseMeleeAttackState : PlayerBaseState
{
    private bool _isAttacking;
    private bool _inAttackState;

    protected PlayerBaseMeleeAttackState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) {}

    protected bool AttackNotPressedAndNotAttacking() 
    {
        _attackPressed = _player.InputHandler.IsInputActiveMeleeAttack;
        _isAttacking = _player.AttackHandler.IsAttacking;
        return !_attackPressed && !_isAttacking;
    }

    protected bool AttackToDodgeCondition() => AttackNotPressedAndNotAttacking() && ToDodgeCondition();

    protected bool AttackToRunCondition() => AttackNotPressedAndNotAttacking() && ToRunCondition();

    protected bool AttackToWalkCondition()
    {
        _movePressed = _player.InputHandler.IsInputActiveMovement;
        _runPressed = _player.InputHandler.IsInputActiveRun;
        return AttackNotPressedAndNotAttacking() && _movePressed && !_runPressed;
    }

    protected bool AttackToIdleCondition()
    {
        _inAttackState = _player.AttackHandler.InAttackState();
        _movePressed = _player.InputHandler.IsInputActiveMovement;
        _runPressed = _player.InputHandler.IsInputActiveRun;
        return !_inAttackState && !_movePressed && !_runPressed;
    }
}