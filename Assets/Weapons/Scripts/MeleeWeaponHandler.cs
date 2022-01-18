using UnityEngine;

public class MeleeWeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject _trailObject;
    [SerializeField] private float _damagePerHit = 25.0f;
    public bool canDamage;

    public float DamagePerHit { get => canDamage ? _damagePerHit : 0; }

    private void Awake() => SetTrailActive(false);

    public void SetTrailActive(bool status) => _trailObject.SetActive(status);
}
