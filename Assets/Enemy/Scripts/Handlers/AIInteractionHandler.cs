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
    }

    private void Start() => _healthBar.InitializeBar(_aiAgent.Health);

    public void SetWeaponDamageStatus(bool status) => _weapon.canDamage = status;

    public void TakeDamage(float damage)
    {
        _aiAgent.Health.Take(damage);
        _healthBar.UpdateBar(_aiAgent.Health);

        if (_aiAgent.Health.IsEmpty())
            _aiAgent.Die();
    }

    #region Handling Death
    public void HideHealthBarAndDropWeapon()
    {
        SetActiveHealthBarUI(false);
        SetWeaponGravity(true);
    }

    public void SetActiveHealthBarUI(bool status) => _healthBar.gameObject.SetActive(status);

    private void SetWeaponGravity(bool status)
    {
        _weapon.GetComponent<Rigidbody>().useGravity = status;
        _weapon.GetComponent<BoxCollider>().isTrigger = !status;
        if (status) _weapon.transform.parent = null;
    }
    #endregion
}
