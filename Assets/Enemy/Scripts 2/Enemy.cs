using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private StatusConfig _health;
    public StatusConfig Health { get => _health; }

    [SerializeField] private BarManager _bar;
    private BaseStatus status;

    private void Start() 
    {
        status = new BaseStatus(_health.maxCapacity);
        _bar.InitializeBar(status);
        _bar.UpdateBar(status);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.TryGetComponent<MeleeWeaponHandler>(out MeleeWeaponHandler weapon))
        {
            if (weapon.canDamage && weapon.Wielder.TryGetComponent<Player>(out Player _))
            {
                _health.Take(weapon.DamagePerHit);
                status.Take(weapon.DamagePerHit);
                _bar.UpdateBar(status);
            }
        }
    }
}
