using UnityEngine;

[CreateAssetMenu(fileName = "AIConfig", menuName = "Gladiator/AIConfig", order = 1)]
public class AIConfig : ScriptableObject {
    
    [Header("Path update parameters")]
    [Tooltip("Minimum amount of time to pass before the next path update is called.")]
    [SerializeField] private float _minimumUpdateWaitTime = 1.0f;
    
    [Tooltip("Minimum amount of units player should travel in order to update path.")]
    [SerializeField] private float _minimumUpdateDistance = 5.0f;
    
    [Tooltip("Minimum distance of awareness of an chasing. If the player is within this distance, the enemy will being chasing.")]
    [SerializeField] private float _awarenessRadius = 1000.0f;
    
    [Tooltip("Minimum distance required to attack player")]
    [SerializeField] private float _attackRadius = 2.0f;

    [Tooltip("Health the enemy starts with")]
    [SerializeField] private float _health = 100.0f;

    [Tooltip("Probability that enemy will transition to attack and not taunt")]
    [SerializeField] [Range(0.0f, 1.0f)] private float _attackChance = 0.5f;

    [Tooltip("Minimum amount of time to wait before initiating next attack")]
    [SerializeField] private float _attackCoolDown = 5.0f;

    [Tooltip("Minimum amount of time to wait before initiating next taunt")]
    [SerializeField] private float _tauntCoolDown = 3.0f;

    [Tooltip("Maximum taunts in a row before an attack")]
    [SerializeField] private int _maxTaunts = 3;

    public float MinimumUpdateWaitTime { get => _minimumUpdateWaitTime; }
    public float MinimumUpdateDistance { get => _minimumUpdateDistance; }
    public float AwarenessRadius { get => _awarenessRadius; }
    public float AttackRadius { get => _attackRadius; }
    public float AttackChance { get => _attackChance; }
    public float AttackCoolDown { get => _attackCoolDown; }
    public float TauntCoolDown { get => _tauntCoolDown; }
    public float MaxTaunts { get => _maxTaunts; }
    public float Health { get => _health; }

}