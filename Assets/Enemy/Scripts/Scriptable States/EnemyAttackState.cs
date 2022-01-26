using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttackState", menuName = "Gladiator/Enemy States/Attack", order = 4)]
public class EnemyAttackState : BaseState<Enemy>
{
    public override bool EntryCondition(Enemy context) => context.AnimationHandler.IsAttacking;

    public override void ExecuteState(Enemy context) {}

    public override void OnEnterState(Enemy context) => context.InteractionHandler.SetWeaponDamageStatus(true);

    public override void OnExitState(Enemy context) 
    {
        context.StateMachine.ResetAttackTimer();
        context.InteractionHandler.SetWeaponDamageStatus(false);
    }
}