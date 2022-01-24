using UnityEngine;

[CreateAssetMenu(fileName = "EnemyIdleState", menuName = "Gladiator/Enemy States/Idle", order = 0)]
public class EnemyIdleState : BaseState<Enemy>
{
    public override bool EntryCondition(Enemy context) => context.Locomotion.IsWithInAttackRange();

    public override void ExecuteState(Enemy context) 
    {
        context.AnimationHandler.SetParameterMovementSpeed(0f);
        context.Locomotion.RotateTowardsPlayer();
    }

    public override void OnEnterState(Enemy context) {}

    public override void OnExitState(Enemy context) {}
}