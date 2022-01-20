using UnityEngine;

public class AIInteractionHandler : MonoBehaviour
{
    [SerializeField] private AIAgent _aiAgent;
    [SerializeField] private BarManager _healthBar;
    [SerializeField] private MeleeWeaponHandler _weapon;
    private float _cumulativeDamage;

    private void OnEnable()
    {
        SetActiveHealthBarUI(true);
        SetWeaponGravity(false);
        AIAgent.OnDeath += HandleOnDeath;
    }

    private void OnDisable() => AIAgent.OnDeath -= HandleOnDeath;

    private void Start() => _healthBar.InitializeBar(_aiAgent.Health);

    public void TakeDamage(float damage)
    {
        _aiAgent.Health.Take(damage);
        _healthBar.UpdateBar(_aiAgent.Health);
    }

    #region Handling Death
    private void HandleOnDeath()
    {
        SetActiveHealthBarUI(false);
        SetWeaponGravity(true);
    }

    public void SetActiveHealthBarUI(bool status) 
    {
        Canvas canvas = _healthBar.GetComponentInParent<Canvas>();
        canvas.gameObject.SetActive(status);
    }

    public void SetWeaponGravity(bool status)
    {
        Rigidbody _rbody = _weapon.GetComponent<Rigidbody>();
        if (_rbody != null) _rbody.useGravity = status;

        BoxCollider _bColl = _weapon.GetComponent<BoxCollider>();
        if (_bColl != null) _bColl.isTrigger = !status;
    }
    #endregion
}
