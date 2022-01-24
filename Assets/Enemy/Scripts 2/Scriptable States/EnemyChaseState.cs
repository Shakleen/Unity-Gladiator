using UnityEngine;

[CreateAssetMenu(fileName = "EnemyChaseState", menuName = "Gladiator/Enemy States/Chase", order = 2)]
public class EnemyChaseState : BaseState<Enemy>
{
    public override bool EntryCondition(Enemy context) => !context.Locomotion.IsWithInAttackRange();

    public override void ExecuteState(Enemy context) 
    {
        context.AnimationHandler.SetParameterMovementSpeed(context.Locomotion.Velocity());
    }

    public override void OnEnterState(Enemy context) => context.Locomotion.SetNavMeshAgentState(true);

    public override void OnExitState(Enemy context) => context.Locomotion.SetNavMeshAgentState(false);
}