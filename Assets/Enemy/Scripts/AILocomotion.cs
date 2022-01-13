using UnityEngine;
using UnityEngine.AI;

public class AILocomotion : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Animator _animator;

    private void Update() { FollowPlayer(); }

    private void FollowPlayer()
    {
        _navMeshAgent.destination = _playerTransform.position;
        _animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude);
    }
}
