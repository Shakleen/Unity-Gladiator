using UnityEngine;

public class PlayerIdleMeleeAttackState : PlayerBaseState
{
    int _attackNumber;

    public PlayerIdleMeleeAttackState(Player context, PlayerStateManager manager) : base(context, manager) {}

    public override void OnEnterState() 
    { 
        hasPrint = false; 
        _attackNumber = 1;
        _context.AnimatorHandler.SetAnimationValueIsMeleeAttacking(true);
        _context.AnimatorHandler.IncrementMeleeAttackNumber();
    }

    public override void OnExitState() 
    { 
        hasPrint = false; 
        _context.AnimatorHandler.SetAnimationValueIsMeleeAttacking(false);
        _context.AnimatorHandler.ResetMeleeAttackNumber();
        _attackNumber = 0;
    }

    public override void CheckSwitchState() 
    {
        if (!_context.AnimatorHandler.IsMeleeAttacking)
        {
            if (_context.InputHandler.IsInputActiveDodge)
                SwitchState(_manager.GetDodgeState());
            else if (_context.InputHandler.IsInputActiveMovement)
            {
                if (_context.InputHandler.IsInputActiveRun)
                    SwitchState(_manager.GetRunState());
                else
                    SwitchState(_manager.GetWalkState());
            }
            else if (!_context.AnimatorHandler.IsAnimationPlaying())
                SwitchState(_manager.GetIdleState());
        }
    }

    public override void ExecuteState()
    {
        CheckSwitchState();
        CheckChainCombo();
    }

    private void CheckChainCombo()
    {
        if (_context.AnimatorHandler.IsMeleeAttacking)
        {
            if (_context.InputHandler.IsInputActiveMeleeAttack && IsAttackNumberEqual())
                _attackNumber++;
        }
        else
        {
            if (!IsAttackNumberEqual())
                _context.AnimatorHandler.IncrementMeleeAttackNumber();
        }
    }

    private bool IsAttackNumberEqual() { return _attackNumber == _context.AnimatorHandler.MeleeAttackNumber; }

    public override string GetName()
    {
        hasPrint = true;
        return "Idle Melee Attack";
    }
}