using UnityEngine;

public class MeleeWeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject _wielder;
    [SerializeField] private GameObject _trailObject;
    [SerializeField] private float _damagePerHit = 25.0f;
    [SerializeField] private AudioClip _slashAudio;
    [SerializeField] private AudioClip _hitAudio;
    private Rigidbody _rigidBody;
    private BoxCollider _boxCollider;
    private AudioSource _audioSource;
    private Transform _startingTransform;

    public bool canDamage;
    public GameObject Wielder { get => _wielder; }

    public float DamagePerHit { get => _damagePerHit; }

    private void Awake() 
    {
        _rigidBody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        _audioSource = GetComponent<AudioSource>();
        SetTrailActive(false);
        _startingTransform = gameObject.transform;
    }

    public void PlaySlashSound() => _audioSource.PlayOneShot(_slashAudio);

    public void PlayHitSound() => _audioSource.PlayOneShot(_hitAudio);

    public void SetTrailActive(bool status) => _trailObject.SetActive(status);
}
