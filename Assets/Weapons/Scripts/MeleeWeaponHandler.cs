using UnityEngine;

public class MeleeWeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject _wielder;
    [SerializeField] private GameObject _trailObject;
    [SerializeField] private float _damagePerHit = 25.0f;
    public bool canDamage;
    public GameObject Wielder { get => _wielder; }

    public float DamagePerHit { get => _damagePerHit; }

    private void Awake() => SetTrailActive(false);

    public void SetTrailActive(bool status) => _trailObject.SetActive(status);
}
