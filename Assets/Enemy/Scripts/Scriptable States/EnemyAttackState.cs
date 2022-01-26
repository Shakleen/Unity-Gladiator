using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttackState", menuName = "Gladiator/Enemy States/Attack", order = 4)]
public class EnemyAttackState : BaseState<Enemy>
{
    private bool _isWithInRange;

    public override bool EntryCondition(Enemy context)
    {
        _isWithInRange = context.Locomotion.IsWithInAttackRange();
        return _isWithInRange && context.StateMachine.IsTimerDone(context.StateMachine.AttackTimer);
    }

    public override void ExecuteState(Enemy context) {}

    public override void OnEnterState(Enemy context) => context.AnimationHandler.SetParameterIsAttacking(true);

    public override void OnExitState(Enemy context) => context.StateMachine.ResetAttackTimer();
}