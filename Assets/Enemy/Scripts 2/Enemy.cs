using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private StatusConfig _health;
    public StatusConfig Health { get => _health; }

    [SerializeField] private GameEvent _onHealthChange;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.TryGetComponent<MeleeWeaponHandler>(out MeleeWeaponHandler weapon))
        {
            if (weapon.canDamage && weapon.Wielder.TryGetComponent<Player>(out Player _))
            {
                _health.Take(weapon.DamagePerHit);
                _onHealthChange.Raise();
            }
        }
    }
}
