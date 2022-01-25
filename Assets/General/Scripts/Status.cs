using UnityEngine;

public class Status
{
    private const float THRESH = 1e-5f;
    
    public float maxCapacity { get; private set; }

    private float _currentCapacity;
    public float currentCapacity 
    { 
        get => _currentCapacity;
        private set => _currentCapacity = Mathf.Clamp(value, 0, maxCapacity);
    }
    public float regenPerSec { get; private set; }
    public float depletePerSec { get; private set; }
    public float regenDelay { get; private set; }

    public Status(StatusConfig status)
    {
        this.maxCapacity = status.maxCapacity;
        this.currentCapacity = maxCapacity;
        this.regenPerSec = status.regenPerSec;
        this.depletePerSec = status.depletePerSec;
        this.regenDelay = status.regenDelay;
    }
    
    public void Add(float value) => currentCapacity += value;

    public void Take(float value) => currentCapacity -= value;

    public void Reset() => currentCapacity = maxCapacity;

    public bool IsEmpty() => currentCapacity <= THRESH; 

    public bool IsFull() => currentCapacity >= maxCapacity; 

    public void Regenerate() => Add(regenPerSec * Time.deltaTime);

    public void Deplete() => Take(depletePerSec * Time.deltaTime);

    public override string ToString()
    {
        return $"Max = {maxCapacity}, Current = {currentCapacity} Regen = {regenPerSec} Deplete = {depletePerSec} delay = {regenDelay}";
    }
}