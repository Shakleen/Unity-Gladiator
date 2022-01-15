using UnityEngine;

public class RegenStatus : BaseStatus
{
    protected float _regenRate;
    protected float _depleteRate;

    public RegenStatus(float maxCapacity, float regenRate, float depleteRate) : base(maxCapacity) 
    { 
        _regenRate = regenRate; 
        _depleteRate = depleteRate;
    }

    public void Regenerate() { Add(_regenRate * Time.deltaTime); }

    public void Deplete() { Take(_depleteRate * Time.deltaTime); }
}