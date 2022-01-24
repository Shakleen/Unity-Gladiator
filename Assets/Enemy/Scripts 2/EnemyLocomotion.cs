using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotion : MonoBehaviour 
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _playerTransform;

    private WaitForSeconds _pathUpdateWaitTime;
    private Vector3 _lookVector;

    private void Start() => StartCoroutine(UpdatePath());

    public void SetNavMeshAgentState(bool status) => _navMeshAgent.enabled = status;

    private IEnumerator UpdatePath()
    {
        _pathUpdateWaitTime = new WaitForSeconds(_enemy.Config.TimeBetweenUpdates);

        while (!_enemy.Config.Health.IsEmpty())
        {
            if (PlayerPositionChange() >= Square(_enemy.Config.MinimumUpdateDistance) && _navMeshAgent.enabled)
                _navMeshAgent.destination = _playerTransform.position;

            yield return _pathUpdateWaitTime;
        }
    }

    public float Velocity() => _navMeshAgent.velocity.magnitude;

    public void RotateTowardsPlayer()
    {
        _lookVector = _playerTransform.position - transform.position;
        _lookVector.y = transform.position.y;
        transform.rotation = Quaternion.Slerp(
            Quaternion.LookRotation(_lookVector), 
            transform.rotation, 
            Time.deltaTime * _enemy.Config.RotationSpeed
        );
    }

    #region Distance checking
    public bool IsWithInAttackRange() => DistanceFromPlayer() <= Square(_enemy.Config.AttackRange);

    public float PlayerPositionChange() => (_playerTransform.position - _navMeshAgent.destination).sqrMagnitude;

    private float DistanceFromPlayer() => (_playerTransform.position - transform.position).sqrMagnitude;

    private float Square(float x) => x * x;
    #endregion
}