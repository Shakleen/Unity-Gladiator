using UnityEngine;

public class EnemyInteractionHandler : MonoBehaviour 
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private GameEvent _onHealthChange;
    [SerializeField] private GameEvent _onDeath;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.TryGetComponent<MeleeWeaponHandler>(out MeleeWeaponHandler weapon))
        {
            if (weapon.canDamage && weapon.Wielder.TryGetComponent<Player>(out Player _))
            {
                _enemy.Health.Take(weapon.DamagePerHit);
                _enemy.audioSource.PlayOneShot(_enemy.Config.Sound.GetRandomPainSound());
                RaiseGameEvent();
            }
        }
    }

    private void RaiseGameEvent()
    {
        if (_enemy.IsDead) return;
        
        _onHealthChange.Raise();
        
        if (_enemy.Health.IsEmpty())
            _onDeath.Raise();
    }
}