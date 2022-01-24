using UnityEngine;

[CreateAssetMenu(fileName = "StatusConfig", menuName = "Gladiator/StatusConfig", order = 0)]
public class StatusConfig : ScriptableObject
{
    private const float THRESH = 1e-3f;

    #region Serialize Fields
    [Tooltip("Maximum status capacity")] [SerializeField] private float _maxCapacity;
    public float maxCapacity { get => _maxCapacity; }

    [Tooltip("Current status capacity")] [SerializeField] private float _currentCapacity;
    public float currentCapacity
    { 
        get => _currentCapacity;
        private set => _currentCapacity = Mathf.Clamp(value, 0, _maxCapacity);
    }

    [Tooltip("Regeneration delay from decrease")] [SerializeField] private float _regenDelay;
    public float regenDelay { get => _regenDelay; }
    
    [Tooltip("Status regeneration rate")] [SerializeField] private float _regenPerSec;
    [Tooltip("Status depletion rate upon usage")] [SerializeField] private float _depletePerSec;
    #endregion

    private void Awake() => _currentCapacity = _maxCapacity;
    
    public void Add(float value) => _currentCapacity += value;

    public void Take(float value) => _currentCapacity -= value;

    public void Reset() => _currentCapacity = _maxCapacity;

    public bool IsEmpty() => _currentCapacity <= THRESH; 

    public bool IsFull() => _currentCapacity >= _maxCapacity; 

    public void Regenerate() => Add(_regenPerSec * Time.deltaTime);

    public void Deplete() => Take(_depletePerSec * Time.deltaTime);
}
