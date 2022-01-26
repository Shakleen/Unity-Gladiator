using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyCountKeeper", menuName = "Gladiator/Enemy/Count keeper", order = 0)]
public class EnemyCountKeeper : ScriptableObject
{
    private List<Enemy> _enemies;
    private int _index;

    private void OnEnable() => _enemies = new List<Enemy>();

    public void Register(Enemy enemy) => _enemies.Add(enemy);

    public void Unregister(Enemy enemy) => _enemies.Remove(enemy);

    public Enemy GetRandomActiveEnemy()
    {
        if (_enemies.Count > 0)
        {
            _index = Random.Range(0, _enemies.Count-1);

            if (IsEnemyEligible(_enemies[_index]))
                return _enemies[_index];
        }

        return null;
    }

    private bool IsEnemyEligible(Enemy enemy)
    {
        if (NotWithInAttackRange(enemy))
            return false;
        if (IsTaunting(enemy))
            return false;
        if (AttackTimerNotExpired(enemy))
            return false;

        return true;
    }

    private static bool AttackTimerNotExpired(Enemy enemy) => !enemy.StateMachine.IsTimerDone(enemy.StateMachine.AttackTimer);

    private static bool IsTaunting(Enemy enemy) => enemy.AnimationHandler.IsTaunting;

    private static bool NotWithInAttackRange(Enemy enemy) => !enemy.Locomotion.IsWithInAttackRange();
}