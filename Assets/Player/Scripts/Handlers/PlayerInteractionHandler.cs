using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour 
{
    [SerializeField] private Player _player; 
    [SerializeField] private MeleeWeaponHandler _weapon;

    private void OnEnable() => PlayerStatusHandler.OnDeath += OnDeath;

    private void OnDisable() => PlayerStatusHandler.OnDeath -= OnDeath;

    private void OnDeath() => SetWeaponGravity(true);

    private void SetWeaponGravity(bool status)
    {
        _weapon.GetComponent<Rigidbody>().useGravity = status;
        _weapon.GetComponent<BoxCollider>().isTrigger = !status;
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