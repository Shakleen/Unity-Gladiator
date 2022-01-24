using UnityEngine;

public class HitBoxHandler : MonoBehaviour
{
    private AIAgent _aiAgent;

    private void Awake() => _aiAgent = GetComponentInParent<AIAgent>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<MeleeWeaponHandler>(out MeleeWeaponHandler weapon))
        {
            if (weapon.canDamage && weapon.Wielder.TryGetComponent<Player>(out Player _))
            {
                weapon.PlayHitSound();
                _aiAgent.InteractionHandler.TakeDamage(weapon.DamagePerHit);
                _aiAgent.audioSource.PlayOneShot(_aiAgent.Config.sounds.GetRandomPainSound());
            }
        }
    }
}
