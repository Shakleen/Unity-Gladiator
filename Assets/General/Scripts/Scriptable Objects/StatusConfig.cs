using UnityEngine;

[CreateAssetMenu(fileName = "StatusConfig", menuName = "Gladiator/StatusConfig", order = 0)]
public class StatusConfig : ScriptableObject
{
    #region Serialize Fields
    [Tooltip("Maximum status capacity")] [SerializeField] private float _maxCapacity;
    public float maxCapacity { get => _maxCapacity; }

    [Tooltip("Current status capacity")] [SerializeField] private float _currentCapacity;
    public float currentCapacity { get => _currentCapacity; }

    [Tooltip("Regeneration delay from decrease")] [SerializeField] private float _regenDelay;
    public float regenDelay { get => _regenDelay; }
    
    [Tooltip("Status regeneration rate")] [SerializeField] private float _regenPerSec;
    public float regenPerSec { get => _regenPerSec; }

    [Tooltip("Status depletion rate upon usage")] [SerializeField] private float _depletePerSec;
    public float depletePerSec { get => _depletePerSec; }
    #endregion
}
