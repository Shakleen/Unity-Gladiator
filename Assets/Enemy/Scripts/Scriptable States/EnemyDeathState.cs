using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDeathState", menuName = "Gladiator/Enemy States/Death", order = 1)]
public class EnemyDeathState : BaseState<Enemy>
{
    public override bool EntryCondition(Enemy context) => context.Health.IsEmpty();

    public override void ExecuteState(Enemy context) 
    {
        if (context.AnimationHandler.IsAnimationPlaying()) return;
        context.HasDied();
    }

    public override void OnEnterState(Enemy context) => context.AnimationHandler.SetParameterIsDead(true);

    public override void OnExitState(Enemy context) {}
}