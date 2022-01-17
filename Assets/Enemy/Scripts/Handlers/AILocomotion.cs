using UnityEngine;
using UnityEngine.AI;

public class AILocomotion : MonoBehaviour 
{
    #region Component references
    [SerializeField] private AIAgent _aiAgent;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    #endregion

    private float _timer;
    
    public NavMeshAgent NavMeshAgent { get => _navMeshAgent; }

    public void UpdateAgentPath()
    {
        _timer -= Time.deltaTime;

        if (_timer < 0f)
        {
            float thresh = _aiAgent.Config.MinimumUpdateDistance * _aiAgent.Config.MinimumUpdateDistance;
            float playerMoveDistance = (_aiAgent.PlayerTransform.position - _navMeshAgent.destination).sqrMagnitude;

            if (playerMoveDistance > thresh)
                _navMeshAgent.destination = _aiAgent.PlayerTransform.position;

            _timer = _aiAgent.Config.MinimumUpdateWaitTime;
        }
    }    
}