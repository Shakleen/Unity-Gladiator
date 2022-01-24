using UnityEngine;

[CreateAssetMenu(fileName = "StatusConfig", menuName = "Gladiator/StatusConfig", order = 0)]
public class StatusConfig : ScriptableObject
{
    private const float THRESH = 1e-3f;

    [Tooltip("Maximum status capacity")] [SerializeField] private float _maxCapacity;
    public float maxCapacity { get => _maxCapacity; }

    [Tooltip("Initial status capacity")] [SerializeField] private float _currentCapacity;
    public float initialCapacity { get => _currentCapacity; }
    
    [Tooltip("Status regeneration rate")] [SerializeField] private float _regenPerSec;
    public float regenPerSec { get => _regenPerSec; }
    
    [Tooltip("Status depletion rate upon usage")] [SerializeField] private float _depletePerSec;
    public float depletePerSec { get => _depletePerSec; }
    
    [Tooltip("Regeneration delay from decrease")] [SerializeField] private float _regenDelay;
    public float regenDelay { get => _regenDelay; }

    private void Awake() => _currentCapacity = _maxCapacity;

    private void OnValidate() => _currentCapacity = Mathf.Clamp(_currentCapacity, 0, _maxCapacity);
    
    public void Add(float value) 
    { 
        _currentCapacity += value;
        _currentCapacity = IsFull() ? _maxCapacity : _currentCapacity;
    }

    public void Take(float value) 
    { 
        _currentCapacity -= value;
        _currentCapacity = IsEmpty() ? 0 : _currentCapacity;
    }

    public void Reset() => _currentCapacity = _maxCapacity;

    public bool IsEmpty() => _currentCapacity <= THRESH; 

    public bool IsFull() => _currentCapacity >= _maxCapacity; 

    public void Regenerate() => Add(_regenPerSec * Time.deltaTime);

    public void Deplete() => Take(_depletePerSec * Time.deltaTime);
}
