using UnityEngine;

public class PlayerWalkingMeleeAttackState : PlayerBaseState
{
    float _lastAttackTime;
    int _attackNumber;

    public PlayerWalkingMeleeAttackState(Player context, PlayerStateManager manager) : base(context, manager) {}

    public override void OnEnterState() 
    { 
        hasPrint = false; 
        _lastAttackTime = Time.time;
        _attackNumber = 1;
        _context.AnimatorHandler.SetAnimationValueIsMeleeAttacking(true);
        _context.AnimatorHandler.IncrementMeleeAttackNumber();
    }

    public override void OnExitState() 
    { 
        hasPrint = false; 
        _context.AnimatorHandler.SetAnimationValueIsMeleeAttacking(false);
        _context.AnimatorHandler.ResetMeleeAttackNumber();
        _lastAttackTime = 0f;
        _attackNumber = 0;
    }

    public override void CheckSwitchState() 
    {
        if (_context.InputHandler.IsInputActiveDodge && !_context.AnimatorHandler.IsMeleeAttacking)
            SwitchState(_manager.GetDodgeState());
        else if (!_context.AnimatorHandler.IsAnimationPlaying())
            SwitchState(_manager.GetIdleState());
    }

    public override void ExecuteState() 
    { 
        CheckSwitchState(); 
        
        if (_context.AnimatorHandler.IsMeleeAttacking)
        {
            if (IsAttackNumberEqual()) _attackNumber++;
        }
        else if (!IsAttackNumberEqual())
        {
            _context.AnimatorHandler.IncrementMeleeAttackNumber();
        }
    }

    private bool IsAttackNumberEqual() { return _attackNumber == _context.AnimatorHandler.MeleeAttackNumber; }

    private bool isWithInComboTime(float attackTime)  { return (attackTime - _lastAttackTime) <= _context.MovementHandler.MeleeAttackComboTimeLimit; }

    public override string GetName()
    {
        hasPrint = true;
        return "Melee Attack";
    }
}