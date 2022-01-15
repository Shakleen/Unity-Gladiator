using UnityEngine;

public class BaseStatus
{
    private float THRESH = 1e-3f;
    protected float _maxCapacity;
    protected float _currentCapacity;

    public float MaxCapacity { get { return _maxCapacity; } }

    public float CurrentCapacity { get { return _currentCapacity; } }

    public BaseStatus(float maxCapacity) { _maxCapacity = _currentCapacity = maxCapacity; }
    
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

    public bool IsEmpty() { return _currentCapacity <= THRESH; }

    public bool IsFull() { return _currentCapacity >= _maxCapacity; }
}