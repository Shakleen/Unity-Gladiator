using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyInteractionHandler _interactionHandler;

    [SerializeField] private EnemyAnimationHandler _animationHandler;
    public EnemyAnimationHandler AnimationHandler { get => _animationHandler; }

    [SerializeField] private EnemyStateMachine _stateMachine;

    [SerializeField] private EnemyLocomotion _locomotion;
    public EnemyLocomotion Locomotion { get => _locomotion; }

    [SerializeField] private AudioSource _audioSource;
    public AudioSource audioSource { get => _audioSource; }

    [SerializeField] private EnemyConfig _config;
    public EnemyConfig Config { get => _config; }

    public Status Health { get; private set; }

    private void Awake()
    {
        Health = new Status(_config.Health);
        Init();
    }

    private void Init() => Health.Reset();

    private void Update() => _stateMachine.Execute();
}
