using UnityEngine;

[CreateAssetMenu(fileName = "EnemyIdleState", menuName = "Gladiator/Enemy States/Idle", order = 0)]
public class EnemyIdleState : BaseState<Enemy>
{
    public override bool EntryCondition(Enemy context) => true;

    public override void ExecuteState(Enemy context) {}

    public override void OnEnterState(Enemy context) {}

    public override void OnExitState(Enemy context) {}
}