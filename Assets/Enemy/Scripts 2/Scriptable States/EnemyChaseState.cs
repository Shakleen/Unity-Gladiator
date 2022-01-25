using UnityEngine;

[CreateAssetMenu(fileName = "EnemyChaseState", menuName = "Gladiator/Enemy States/Chase", order = 2)]
public class EnemyChaseState : BaseState<Enemy>
{
    private bool _isWithInAttackRange;
    public override bool EntryCondition(Enemy context) 
    {
        _isWithInAttackRange = context.Locomotion.IsWithInAttackRange();
        return !_isWithInAttackRange && !context.AnimationHandler.IsTaunting;
    }

    public override void ExecuteState(Enemy context) 
    {
        context.AnimationHandler.SetParameterMovementSpeed(context.Locomotion.Velocity());
        context.Locomotion.RotateTowardsPlayer();
    }

    public override void OnEnterState(Enemy context) => context.Locomotion.SetNavMeshAgentState(true);

    public override void OnExitState(Enemy context) => context.Locomotion.SetNavMeshAgentState(false);
}