using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHandlerHUD : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private TextMeshProUGUI _healthValueText;
    [SerializeField] private Slider _staminaBar;
    [SerializeField] private TextMeshProUGUI _staminaValueText;
    [SerializeField] private Slider _manaBar;
    [SerializeField] private TextMeshProUGUI _manaValueText;
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

    public void UpdateHealthBar()
    {
        _healthBar.value = _statusHandler.Health.CurrentCapacity;
        _healthValueText.text = FormatStatusDisplayString(
            (int) _statusHandler.Health.CurrentCapacity, 
            (int) _statusHandler.Health.MaxCapacity
        );
    }

    public void UpdateStaminaBar() 
    { 
        _staminaBar.value = _statusHandler.Stamina.CurrentCapacity; 
        _staminaValueText.text = FormatStatusDisplayString(
            (int) _statusHandler.Stamina.CurrentCapacity, 
            (int) _statusHandler.Stamina.MaxCapacity
        );
    }

    public void UpdateManaBar() 
    { 
        _manaBar.value = _statusHandler.Mana.CurrentCapacity; 
        _manaValueText.text = FormatStatusDisplayString(
            (int) _statusHandler.Mana.CurrentCapacity, 
            (int) _statusHandler.Mana.MaxCapacity
        );
    }

    private string FormatStatusDisplayString(float current, float max) => string.Format("{0} / {1}", (int)current, (int)max);
}
