using UnityEngine;

[CreateAssetMenu(fileName = "StatusConfig", menuName = "Gladiator/StatusConfig", order = 0)]
public class StatusConfig : ScriptableObject
{
    [Tooltip("Initial status capacity")] [SerializeField] private float _initialCapacity;
    public float initialCapacity { get { return _initialCapacity; } }
    
    [Tooltip("Status regeneration rate")] [SerializeField] private float _regenPerSec;
    public float regenPerSec { get { return _regenPerSec; } }
    
    [Tooltip("Status depletion rate upon usage")] [SerializeField] private float _depletePerSec;
    public float depletePerSec { get { return _depletePerSec; } }
}
