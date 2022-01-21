using UnityEngine;

public class MeleeWeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject _wielder;
    [SerializeField] private GameObject _trailObject;
    [SerializeField] private float _damagePerHit = 25.0f;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private BoxCollider _boxCollider;
    private Transform _startingTransform;

    public bool canDamage;
    public GameObject Wielder { get => _wielder; }

    public float DamagePerHit { get => _damagePerHit; }

    private void Awake() 
    {
        SetTrailActive(false);
        _startingTransform = gameObject.transform;
    }

    public void SetTrailActive(bool status) => _trailObject.SetActive(status);

    public void DropWeapon()
    {
        _rigidBody.useGravity = true;
        _boxCollider.isTrigger = false;
        transform.parent = null;
    }

    public void ReturnToWeilder()
    {
        _rigidBody.useGravity = false;
        _boxCollider.isTrigger = true;
        transform.parent = _wielder.transform;
        transform.position = _startingTransform.position;
        transform.rotation = _startingTransform.rotation;
    }
}
