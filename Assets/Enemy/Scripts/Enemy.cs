using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyInteractionHandler _interactionHandler;
    public EnemyInteractionHandler InteractionHandler { get => _interactionHandler; }

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
    [SerializeField] private GameEvent _onDeath;

    public Status Health { get; private set; }

    public bool IsDead { get; private set; }

    private WaitForSeconds _deactivationDelay;

    private void Awake()
    {
        _deactivationDelay = new WaitForSeconds(Config.DeactivationDelay);
        Health = new Status(_config.Health);
    }

    public void OnEnable() 
    { 
        IsDead = false;
        Health.Reset(); 
        SetComponentEnabled(true);
    }

    public void OnDisable() => SetComponentEnabled(false);

    private void SetComponentEnabled(bool status)
    {
        _interactionHandler.enabled = status;
        _animationHandler.enabled = status;
        _locomotion.enabled = status;
        _audioSource.enabled = status;
        _collider.enabled = status;
        _stateMachine.enabled = status;
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
        _onDeath.Raise();
        StartCoroutine(Deactivate());
    }

    private IEnumerator Deactivate() 
    {
        yield return _deactivationDelay;
        gameObject.SetActive(false);
    }
}
