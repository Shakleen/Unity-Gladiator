using System;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    #region Component references
    [SerializeField] private AIConfig _config;
    [SerializeField] private AIAnimationHandler _animationHandler;
    [SerializeField] private RagDollHandler _ragDollHandler;
    [SerializeField] private AILocomotion _aiLocomotion;
    [SerializeField] private AIStateEnum _currentStateType;
    [SerializeField] private AIInteractionHandler _interactionHandler;
    private Transform _playerTransform;
    private AIStateMachine _stateMachine;
    private BaseStatus _health;
    private bool _hasDied = false;
    #endregion

    #region Getters and setter
    public AIConfig Config { get => _config; }
    public Transform PlayerTransform { get => _playerTransform; }
    public AIAnimationHandler AnimationHandler { get => _animationHandler; }
    public RagDollHandler RagDollHandler { get => _ragDollHandler; }
    public AILocomotion locomotion { get => _aiLocomotion; }
    public AIInteractionHandler InteractionHandler { get => _interactionHandler; }
    public AIStateEnum CurrentState { get => _currentStateType; set => _currentStateType = value; }
    public BaseStatus Health { get => _health; }
    #endregion

    private void Awake() 
    { 
        _playerTransform = FindObjectOfType<Player>().transform;
        _stateMachine = new AIStateMachine(this); 
        _health = new BaseStatus(_config.Health);
    }

    private void Update()
    {
        if (!_hasDied)
        {
            _stateMachine.ExecuteState();
            _aiLocomotion.DecreaseTimer();
            _aiLocomotion.MoveAgent();
        }
    }

    public void Die()
    {
        if (!_hasDied)
        {
            _hasDied = true;
            FindObjectOfType<GameManager>().AddScore(Config.EnemyValue);
            _ragDollHandler.SetRagDollStatus(true);
            _interactionHandler.HideHealthBarAndDropWeapon();
        }
    }
}
