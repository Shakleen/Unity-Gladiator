using UnityEngine;

public class AIInteractionHandler : MonoBehaviour
{
    [SerializeField] private AIAgent _aiAgent;
    [SerializeField] private BarManager _healthBar;
    [SerializeField] private MeleeWeaponHandler _weapon;
    private float _cumulativeDamage;

    public void SetWeaponDamageStatus(bool status) => _weapon.canDamage = status;

    public void TakeDamage(float damage)
    {
        _aiAgent.Health.Take(damage);
        _healthBar.UpdateBar(_aiAgent.Health);

        if (_aiAgent.Health.IsEmpty())
            _aiAgent.Die();
    }

    #region Handling Death
    public void ShowHealthBarAndWeapon(bool status)
    {
        SetActiveHealthBarUI(status);
        _weapon.gameObject.SetActive(status);
    }

    public void SetActiveHealthBarUI(bool status) 
    {
        _healthBar.gameObject.SetActive(status);
        _healthBar.InitializeBar(_aiAgent.Health);
        _healthBar.UpdateBar(_aiAgent.Health);
    }
    #endregion
}
