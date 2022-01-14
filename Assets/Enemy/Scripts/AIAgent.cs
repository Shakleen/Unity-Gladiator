using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    [SerializeField] private AIConfig _config;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private AIAnimationHandler _animationHandler;
    [SerializeField] private AIStateType _currentStateType;

    private AIStateMachine _stateMachine;
    private BaseStatus _health;

    public AIConfig Config { get { return _config; } }
    public Transform PlayerTransform { get { return _playerTransform; } }
    public NavMeshAgent NavMeshAgent { get { return _navMeshAgent; } }
    public AIAnimationHandler AnimationHandler { get { return _animationHandler; } }
    public BaseStatus Health { get { return _health; } }

    private void Awake() 
    { 
        _stateMachine = new AIStateMachine(this); 
        _health = new BaseStatus(_config.Health);
    }

    private void Update() 
    { 
        _currentStateType = _stateMachine.GetCurrentStateType();
        _stateMachine.ExecuteState(); 
    }
}
