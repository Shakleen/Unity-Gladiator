using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Gladiator/Enemy/Config", order = 0)]
public class EnemyConfig : ScriptableObject 
{
    [Tooltip("Time between successive nav mesh path updates")] [SerializeField] private float _timeBetweenUpdates = 0.1f;
    public float TimeBetweenUpdates { get => _timeBetweenUpdates; }
    
    [Tooltip("Minimum amount of units player should travel in order to update path.")]
    [SerializeField] private float _minimumUpdateDistance = 5.0f;
    public float MinimumUpdateDistance { get => _minimumUpdateDistance; }
    
    [Tooltip("Minimum distance required to attack player")] [SerializeField] private float _attackRange = 2.0f;
    public float AttackRange { get => _attackRange; }
    
    [Tooltip("Speed at which enemy rotates to face player")] [SerializeField] private float _rotationSpeed = 3.0f;
    public float RotationSpeed { get => _rotationSpeed; }

    [Tooltip("Sound collection to be used by the enemy")] [ SerializeField] private Sounds _sound;
    public Sounds Sound { get => _sound; }

    [SerializeField] private StatusConfig _health;
    public StatusConfig Health { get => _health; }   
}