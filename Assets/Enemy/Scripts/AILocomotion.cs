using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AILocomotion : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Animator _animator;
    
    [Tooltip("Amount of time to pass before the next path update is called.")]
    [SerializeField] [Range(0.2f, 10.0f)] private float _minimumUpdateWaitTime = 1.0f;
    
    [Tooltip("Minimum amount of units player should travel in order to call a path update.")]
    [SerializeField] [Range(1.0f, 10.0f)] private float _minimumUpdateDistance = 5.0f;

    private int _animationHashMovementSpeed;

    private void Awake()
    {
        _animationHashMovementSpeed = Animator.StringToHash("MovementSpeed");
        StartCoroutine(UpdatePlayerFollowPath());
    }

    private IEnumerator UpdatePlayerFollowPath()
    {
        float thresh = _minimumUpdateDistance * _minimumUpdateDistance;
        
        while(true)
        {
            float playerMoveDistance = (_playerTransform.position - _navMeshAgent.destination).sqrMagnitude;
            
            if (playerMoveDistance > thresh)
                _navMeshAgent.destination = _playerTransform.position;

            yield return new WaitForSeconds(_minimumUpdateWaitTime);
        }
    }

    private void Update() { _animator.SetFloat(_animationHashMovementSpeed, _navMeshAgent.velocity.magnitude); }
}
