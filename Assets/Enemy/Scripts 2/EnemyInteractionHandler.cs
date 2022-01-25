using UnityEngine;

public class EnemyInteractionHandler : MonoBehaviour 
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private GameEvent _onHealthChange;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.TryGetComponent<MeleeWeaponHandler>(out MeleeWeaponHandler weapon))
        {
            if (weapon.canDamage && weapon.Wielder.TryGetComponent<Player>(out Player _))
            {
                _enemy.Config.Health.Take(weapon.DamagePerHit);
                _enemy.audioSource.PlayOneShot(_enemy.Config.Sound.GetRandomPainSound());
                _onHealthChange.Raise();
            }
        }
    }
}