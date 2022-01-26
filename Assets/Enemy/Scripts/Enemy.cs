using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyInteractionHandler _interactionHandler;

    [SerializeField] private EnemyAnimationHandler _animationHandler;
    public EnemyAnimationHandler AnimationHandler { get => _animationHandler; }

    [SerializeField] private EnemyStateMachine _stateMachine;
    public EnemyStateMachine StateMachine { get => _stateMachine; }

    [SerializeField] private EnemyLocomotion _locomotion;
    public EnemyLocomotion Locomotion { get => _locomotion; }

    [SerializeField] private AudioSource _audioSource;
    public AudioSource audioSource { get => _audioSource; }

    [SerializeField] private EnemyConfig _config;
    public EnemyConfig Config { get => _config; }

    [SerializeField] private CapsuleCollider _collider;

    public Status Health { get; private set; }

    public bool IsDead { get; private set; }

    private void Awake()
    {
        Health = new Status(_config.Health);
        Init();
    }

    public void Init() 
    { 
        Health.Reset(); 
        IsDead = false;
        SetComponentEnabled(true);
    }

    private void Update() 
    {
        if (IsDead) return;
        _stateMachine.Execute();
    }

    public void HasDied()
    {
        if (IsDead) return;

        IsDead = true;
        SetComponentEnabled(false);
    }

    private void SetComponentEnabled(bool status)
    {
        _interactionHandler.enabled = status;
        _animationHandler.enabled = status;
        _locomotion.enabled = status;
        _audioSource.enabled = status;
        _collider.enabled = status;
    }
}
