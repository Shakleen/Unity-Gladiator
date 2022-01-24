using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotion : MonoBehaviour 
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _playerTransform;

    private WaitForSeconds _pathUpdateWaitTime;

    private void Start() => StartCoroutine(UpdatePath());

    public void SetNavMeshAgentState(bool status) => _navMeshAgent.enabled = status;

    private IEnumerator UpdatePath()
    {
        _pathUpdateWaitTime = new WaitForSeconds(_enemy.Config.TimeBetweenUpdates);

        while (!_enemy.Config.Health.IsEmpty())
        {
            _navMeshAgent.destination = _playerTransform.position;
            yield return _pathUpdateWaitTime;
        }
    }

    public float Velocity() => _navMeshAgent.velocity.magnitude;

    #region Distance checking
    public bool IsWithInAttackRange() => DistanceFromPlayer() <= Square(_enemy.Config.AttackRange);

    private float DistanceFromPlayer() => (_playerTransform.position - transform.position).sqrMagnitude;

    private float Square(float x) => x * x;
    #endregion
}