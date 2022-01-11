using UnityEngine;

public class PlayerMeleeAttackState : PlayerBaseState
{
    public PlayerMeleeAttackState(Player context, PlayerStateManager manager) : base(context, manager) {}

    public override void OnEnterState() 
    { 
        hasPrint = false; 
        _context.AnimatorHandler.SetAnimationValueIsMeleeAttacking(true);
        _context.AnimatorHandler.IncrementMeleeAttackNumber();
    }

    public override void OnExitState() 
    { 
        hasPrint = false; 
        _context.AnimatorHandler.SetAnimationValueIsMeleeAttacking(false);
        _context.AnimatorHandler.ResetMeleeAttackNumber();
    }

    public override void CheckSwitchState() 
    {
        if (!_context.AnimatorHandler.IsMeleeAttacking)
        {
            if (!_context.InputHandler.IsInputActiveAttack)
                SwitchState(_manager.GetIdleState());
            else if (_context.InputHandler.IsInputActiveDodge)
                SwitchState(_manager.GetDodgeState());
        }
    }

    public override void ExecuteState() 
    { 
        CheckSwitchState(); 
    }

    public override string GetName()
    {
        hasPrint = true;
        return "Melee Attack";
    }
}