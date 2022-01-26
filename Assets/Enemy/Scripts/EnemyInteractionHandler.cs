using UnityEngine;

public class EnemyInteractionHandler : MonoBehaviour 
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private GameEvent _onHealthChange;
    [SerializeField] private MeleeWeaponHandler _weapon;

    private void OnEnable() => _onHealthChange.Raise();

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.TryGetComponent<MeleeWeaponHandler>(out MeleeWeaponHandler weapon))
        {
            if (weapon.canDamage && weapon.Wielder.TryGetComponent<Player>(out Player _))
            {
                _enemy.Health.Take(weapon.DamagePerHit);
                _enemy.audioSource.PlayOneShot(_enemy.Config.Sound.GetRandomPainSound());
                _onHealthChange.Raise();
            }
        }
    }

    public void SetWeaponDamageStatus(bool status) => _weapon.canDamage = status;
}