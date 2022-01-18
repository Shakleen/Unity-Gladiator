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

    public float MinimumUpdateWaitTime { get { return _minimumUpdateWaitTime; } }
    public float MinimumUpdateDistance { get { return _minimumUpdateDistance; } }
    public float AwarenessRadius { get { return _awarenessRadius; } }
    public float AttackRadius { get { return _attackRadius; } }
    public float Health { get { return _health; } }
    
}