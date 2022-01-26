using UnityEngine;

[CreateAssetMenu(fileName = "EnemyChaseState", menuName = "Gladiator/Enemy States/Chase", order = 2)]
public class EnemyChaseState : BaseState<Enemy>
{
    private bool _isWithInRange, _isTaunting, _isAttacking;

    public override bool EntryCondition(Enemy context) 
    {
        _isWithInRange = context.Locomotion.IsWithInAttackRange();
        _isTaunting = context.AnimationHandler.IsTaunting;
        _isAttacking = context.AnimationHandler.IsAttacking;
        return !_isTaunting && !_isAttacking && !_isWithInRange;
    }

    public override void ExecuteState(Enemy context) 
    {
        context.AnimationHandler.SetParameterMovementSpeed(context.Locomotion.Velocity());
        context.Locomotion.RotateTowardsPlayer();
    }

    public override void OnEnterState(Enemy context) => context.Locomotion.SetNavMeshAgentState(true);

    public override void OnExitState(Enemy context) => context.Locomotion.SetNavMeshAgentState(false);
}