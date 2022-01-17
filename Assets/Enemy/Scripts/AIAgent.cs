using UnityEngine;

public class AIAgent : MonoBehaviour
{
    #region Component references
    [SerializeField] private AIConfig _config;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private AIAnimationHandler _animationHandler;
    [SerializeField] private RagDollHandler _ragDollHandler;
    [SerializeField] private AILocomotion _aiLocomotion;
    [SerializeField] private AIStateType _currentStateType;
    private AIStateMachine _stateMachine;
    private BaseStatus _health;
    #endregion

    #region Getters and setter
    public AIConfig Config { get => _config; }
    public Transform PlayerTransform { get => _playerTransform; }
    public AIAnimationHandler AnimationHandler { get => _animationHandler; }
    public RagDollHandler RagDollHandler { get => _ragDollHandler; }
    public AILocomotion AILocomotion { get => _aiLocomotion; }
    public AIStateType CurrentState { get => _currentStateType; set => _currentStateType = value; }
    public BaseStatus Health { get => _health; }
    #endregion

    private void Awake() 
    { 
        _stateMachine = new AIStateMachine(this); 
        _health = new BaseStatus(_config.Health);
    }

    private void Update() 
    { 
        _stateMachine.ExecuteState(); 
    }
}
