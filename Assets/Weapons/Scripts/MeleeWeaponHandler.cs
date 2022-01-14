using UnityEngine;

public class MeleeWeaponHandler : MonoBehaviour
{
    [SerializeField] private float _damagePerHit = 25.0f;

    public float DamagePerHit { get { return _damagePerHit; } }
}
