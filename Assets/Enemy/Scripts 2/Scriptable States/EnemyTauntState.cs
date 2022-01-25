using UnityEngine;

[CreateAssetMenu(fileName = "EnemyTauntState", menuName = "Gladiator/Enemy States/Taunt", order = 3)]
public class EnemyTauntState : BaseState<Enemy>
{
    private bool _isWithInRange;

    public override bool EntryCondition(Enemy context)
    {
        _isWithInRange = context.Locomotion.IsWithInAttackRange();
        return _isWithInRange && context.StateMachine.IsTimerDone(context.StateMachine.TauntTimer);
    }

    public override void ExecuteState(Enemy context) {}

    public override void OnEnterState(Enemy context) => context.AnimationHandler.SetParameterIsTaunting(true);

    public override void OnExitState(Enemy context) => context.StateMachine.ResetTauntTimer();
}