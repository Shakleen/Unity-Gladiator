using UnityEngine;

[CreateAssetMenu(fileName = "EnemyIdleState", menuName = "Gladiator/Enemy States/Idle", order = 0)]
public class EnemyIdleState : BaseState<Enemy>
{
    private bool _isWithInRange, _isTaunting, _isAttacking;

    public override bool EntryCondition(Enemy context) 
    {
        _isWithInRange = context.Locomotion.IsWithInAttackRange();
        _isTaunting = context.AnimationHandler.IsTaunting;
        _isAttacking = context.AnimationHandler.IsAttacking;
        return !_isTaunting && !_isAttacking && _isWithInRange;
    }

    public override void ExecuteState(Enemy context) 
    {
        context.Locomotion.RotateTowardsPlayer();
    }

    public override void OnEnterState(Enemy context) {}

    public override void OnExitState(Enemy context) {}
}