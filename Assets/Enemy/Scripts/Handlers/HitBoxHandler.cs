using UnityEngine;

public class HitBoxHandler : MonoBehaviour
{
    private AIAgent _aiAgent;

    private void Awake() => _aiAgent = GetComponentInParent<AIAgent>();

    private void OnTriggerEnter(Collider other)
    {
        if (HasWeaponTag(other))
        {
            MeleeWeaponHandler weapon = other.GetComponent<MeleeWeaponHandler>();

            if (weapon != null && weapon.canDamage && DoesPlayerWieldWeapon(weapon))
                _aiAgent.InteractionHandler.TakeDamage(weapon.DamagePerHit);
        }
    }

    private static bool HasWeaponTag(Collider other) => other.gameObject.tag == "Weapon";

    private static bool DoesPlayerWieldWeapon(MeleeWeaponHandler weapon) => weapon.Wielder.GetComponent<Player>() != null;
}
