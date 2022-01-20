using UnityEngine;

public class UIHandlerHUD : MonoBehaviour
{
    [SerializeField] private BarManager _healthBar;
    [SerializeField] private BarManager _staminaBar;
    [SerializeField] private PlayerStatusHandler _statusHandler;

    private void OnEnable() 
    {
        PlayerStatusHandler.OnHealthChange += UpdateHealthBar;
        PlayerStatusHandler.OnStaminaChange += UpdateStaminaBar;
    }

    private void OnDisable() 
    {
        PlayerStatusHandler.OnHealthChange -= UpdateHealthBar;
        PlayerStatusHandler.OnStaminaChange -= UpdateStaminaBar;
    }

    private void Start()
    {
        _healthBar.InitializeBar(_statusHandler.Health);
        _staminaBar.InitializeBar(_statusHandler.Stamina);
    }

    public void UpdateHealthBar() => _healthBar.UpdateBar(_statusHandler.Health);
    public void UpdateStaminaBar() => _staminaBar.UpdateBar(_statusHandler.Stamina);
}
