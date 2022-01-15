using UnityEngine;
using UnityEngine.UI;

public class UIHandlerHUD : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider _staminaBar;
    [SerializeField] private Slider _manaBar;
    [SerializeField] private PlayerStatusHandler _statusHandler;

    private void Start()
    {
        InitSlider(_healthBar, _statusHandler.Health);
        InitSlider(_staminaBar, _statusHandler.Stamina);
        InitSlider(_manaBar, _statusHandler.Mana);
    }

    private void InitSlider(Slider slider, BaseStatus status)
    {
        slider.maxValue = status.MaxCapacity;
        slider.value = status.CurrentCapacity;
    }

    public void UpdateHealthBar() { _healthBar.value = _statusHandler.Health.CurrentCapacity; }

    public void UpdateStaminaBar() { _staminaBar.value = _statusHandler.Stamina.CurrentCapacity; }

    public void UpdateManaBar() { _manaBar.value = _statusHandler.Mana.CurrentCapacity; }
}
