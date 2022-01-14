using UnityEngine;

public class BaseStatus
{
    protected float _maxCapacity;
    protected float _currentCapacity;

    public float MaxCapacity { get { return _maxCapacity; } }

    public float CurrentCapacity { get { return _currentCapacity; } }

    public BaseStatus(float maxCapacity) { _maxCapacity = _currentCapacity = maxCapacity; }
    
    public void Add(float value) { _currentCapacity = Mathf.Min(_maxCapacity, _currentCapacity + value); }

    public void Take(float value) { _currentCapacity = Mathf.Max(0, _currentCapacity - value); }

    public bool IsEmpty() { return _currentCapacity == 0; }

    public bool IsFull() { return _currentCapacity == _maxCapacity; }
}