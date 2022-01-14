using UnityEngine;

public class HitBoxHandler : MonoBehaviour
{
    private AIAgent _aiAgent;

    private void Awake() { _aiAgent = GetComponentInParent<AIAgent>(); }

    private void OnTriggerEnter(Collider other) {
        
        if (other.gameObject.tag == "Weapon")
        {
            Debug.Log("Weapon hit me!");
            MeleeWeaponHandler weapon = other.GetComponent<MeleeWeaponHandler>();
            
            if (weapon != null)
                _aiAgent.Health.Take(weapon.DamagePerHit);
        }
    }
}
