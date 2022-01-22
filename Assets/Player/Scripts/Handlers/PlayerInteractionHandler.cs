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
        if (HasWeaponTag(other))
        {
            MeleeWeaponHandler weapon = other.GetComponent<MeleeWeaponHandler>();

            if (weapon != null && weapon.canDamage && DoesEnemyWieldWeapon(weapon))
                _player.StatusHandler.TakeDamage(weapon.DamagePerHit);
        }
    }

    private static bool HasWeaponTag(Collider other) => other.gameObject.tag == "Weapon";

    private static bool DoesEnemyWieldWeapon(MeleeWeaponHandler weapon) => weapon.Wielder.GetComponent<AIAgent>() != null;
}