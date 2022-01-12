using UnityEngine;

public class PlayerWalkingMeleeAttackState : PlayerBaseState
{
    public PlayerWalkingMeleeAttackState(Player context, PlayerStateManager manager) : base(context, manager) {}

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

    public override void ExecuteState() { CheckSwitchState(); }

    public override string GetName()
    {
        hasPrint = true;
        return "Walking Melee Attack";
    }
}