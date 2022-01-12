using UnityEngine;

public class PlayerRunningMeleeAttackState : PlayerBaseState
{
    public PlayerRunningMeleeAttackState(Player context, PlayerStateManager manager) : base(context, manager) {}

    public override void OnEnterState() 
    { 
        hasPrint = false; 
        _context.AnimatorHandler.SetAnimationValueIsMeleeAttacking(true);
    }

    public override void OnExitState() 
    { 
        hasPrint = false; 
        _context.AnimatorHandler.SetAnimationValueIsMeleeAttacking(false);
        _context.AnimatorHandler.ResetMeleeAttackNumber();
    }

    public override void CheckSwitchState() 
    {
        if (!_context.AnimatorHandler.IsMeleeAttacking && !_context.InputHandler.IsInputActiveMeleeAttack)
        {
            if (_context.InputHandler.IsInputActiveMovement)
            {
                if (_context.InputHandler.IsInputActiveRun)
                    SwitchState(_manager.GetRunState());
                else
                    SwitchState(_manager.GetWalkState());
            }
            else if (!_context.AnimatorHandler.IsAnimationPlaying())
            {
                _context.MovementHandler.StopMovement();
                SwitchState(_manager.GetIdleState());
            }
        }
    }

    public override void ExecuteState() { CheckSwitchState(); }

    public override string GetName()
    {
        hasPrint = true;
        return "Melee Attack";
    }
}