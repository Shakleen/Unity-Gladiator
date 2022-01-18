using UnityEngine;

public class MeleeWeaponHandler : MonoBehaviour
{
    [SerializeField] private float _damagePerHit = 25.0f;
    public bool canDamage;

    public float DamagePerHit { get => canDamage ? _damagePerHit : 0; }
}
