using UnityEngine;

public class AIInteractionHandler : MonoBehaviour
{
    [SerializeField] private AIAgent _aiAgent;
    [SerializeField] private BarManager _healthBar;
    [SerializeField] private MeleeWeaponHandler _weapon;
    private float _cumulativeDamage;

    private void Start()
    {
        SetActiveHealthBarUI(true);
        _healthBar.InitializeBar(_aiAgent.Health);
    }

    public void SetWeaponDamageStatus(bool status) => _weapon.canDamage = status;

    public void TakeDamage(float damage)
    {
        _aiAgent.Health.Take(damage);
        _healthBar.UpdateBar(_aiAgent.Health);

        if (_aiAgent.Health.IsEmpty())
            _aiAgent.Die();
    }

    public void AttachWeapon() => _weapon.ReturnToWeilder();

    #region Handling Death
    public void HideHealthBarAndDropWeapon()
    {
        SetActiveHealthBarUI(false);
        _weapon.DropWeapon();
    }

    public void SetActiveHealthBarUI(bool status) => _healthBar.gameObject.SetActive(status);
    #endregion
}
