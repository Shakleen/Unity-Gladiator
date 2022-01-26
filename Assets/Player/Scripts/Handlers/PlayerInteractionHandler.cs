using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInteractionHandler : MonoBehaviour 
{
    private Player _player; 
    private MeleeWeaponHandler _weapon;

    private void Awake() 
    {
        _player = GetComponent<Player>();
        _weapon = GetComponentInChildren<MeleeWeaponHandler>();    
    }

    private void OnEnable() => PlayerStatusHandler.OnDeath += OnDeath;

    private void OnDisable() => PlayerStatusHandler.OnDeath -= OnDeath;

    private void OnDeath() => SetWeaponGravity(true);

    private void SetWeaponGravity(bool status)
    {
        _weapon.GetComponent<Rigidbody>().useGravity = status;
        _weapon.GetComponent<BoxCollider>().isTrigger = !status;
        _weapon.transform.parent = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<MeleeWeaponHandler>(out MeleeWeaponHandler weapon))
        {
            if (weapon.canDamage && weapon.Wielder.TryGetComponent<Enemy>(out Enemy _))
            {
                weapon.PlayHitSound();
                _player.StatusHandler.TakeDamage(weapon.DamagePerHit);
                _player.audioSource.PlayOneShot(_player.Config.sounds.GetRandomPainSound());
            }
        }
    }
}