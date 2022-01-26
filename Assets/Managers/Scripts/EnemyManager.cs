using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour 
{
    [SerializeField] private SessionData _session;
    [SerializeField] private EnemyCountKeeper _enemyCountKeeper;
    
    private Enemy _enemy;
    private WaitForSeconds _delayBetweenAttacks, _delayBetweenTries;
    private IEnumerator _attackCoroutine;

    private void Awake() 
    {
        _delayBetweenAttacks = new WaitForSeconds(_session.EnemyAttackInterregnum);
        _delayBetweenTries = new WaitForSeconds(1f);
    }

    public void BeginEnemyAttacks()
    {
        if (_attackCoroutine != null)
            StopCoroutine(_attackCoroutine);
    
        _attackCoroutine = EnemyAttackCoroutine();
        StartCoroutine(_attackCoroutine);
    }

    public void StopEnemyAttacks() => StopCoroutine(_attackCoroutine);

    private IEnumerator EnemyAttackCoroutine()
    {
        while (true)
        {
            _enemy = _enemyCountKeeper.GetRandomActiveEnemy();

            if (_enemy != null)
            {
                _enemy.AnimationHandler.SetParameterIsAttacking(true);
                Debug.Log($"{_enemy.gameObject.name} attacking");
                yield return _delayBetweenAttacks;
            }
            else
                yield return _delayBetweenTries;
        }
    }
}