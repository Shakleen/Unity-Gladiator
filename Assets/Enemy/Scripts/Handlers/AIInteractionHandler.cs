using UnityEngine;

public class AIInteractionHandler : MonoBehaviour
{
    [SerializeField] private AIAgent _aiAgent;
    [SerializeField] private BarManager _healthBar;
    [SerializeField] private MeleeWeaponHandler _weapon;
    private float _cumulativeDamage;

    public void SetWeaponDamageStatus(bool status) 
    {
        _weapon.canDamage = status;

        if (status) _weapon.PlaySlashSound();
    }

    public void TakeDamage(float damage)
    {
        // _aiAgent.Config.Health.Take(damage);
        // _healthBar.UpdateValue(_aiAgent.Health);

        // if (_aiAgent.Config.Health.IsEmpty())
            // _aiAgent.Die();
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
        // _healthBar.InitializeMaxValue(_aiAgent.Health);
        // _healthBar.UpdateValue(_aiAgent.Health);
    }
    #endregion
}
