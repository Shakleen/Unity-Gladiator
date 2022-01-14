using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private AIConfig _config;
    [SerializeField] private AIAnimationHandler _animationHandler;
    [SerializeField] private AIStateType _currentStateType;

    private AIStateMachine _stateMachine;

    public AIConfig Config { get { return _config; } }
    public Transform PlayerTransform { get { return _playerTransform; } }
    public NavMeshAgent NavMeshAgent { get { return _navMeshAgent; } }
    public AIAnimationHandler AnimationHandler { get { return _animationHandler; } }

    private void Awake() { _stateMachine = new AIStateMachine(this); }

    private void Update() 
    { 
        _currentStateType = _stateMachine.GetCurrentStateType();
        _stateMachine.ExecuteState(); 
    }
}
