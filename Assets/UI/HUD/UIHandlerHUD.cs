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

    private void OnEnable() 
    {
        PlayerStatusHandler.OnHealthChange += UpdateHealthBar;
        PlayerStatusHandler.OnStaminaChange += UpdateStaminaBar;
        PlayerStatusHandler.OnManaChange += UpdateManaBar;
    }

    private void OnDisable() 
    {
        PlayerStatusHandler.OnHealthChange -= UpdateHealthBar;
        PlayerStatusHandler.OnStaminaChange -= UpdateStaminaBar;
        PlayerStatusHandler.OnManaChange -= UpdateManaBar;
    }

    private void Start()
    {
        InitSlider(_healthBar, _healthValueText, _statusHandler.Health);
        InitSlider(_staminaBar, _staminaValueText, _statusHandler.Stamina);
        InitSlider(_manaBar, _manaValueText, _statusHandler.Mana);
    }

    private void InitSlider(Slider bar, TextMeshProUGUI uiText, BaseStatus status)
    {
        bar.maxValue = status.MaxCapacity;
        bar.value = status.CurrentCapacity;
        uiText.text = FormatStatusDisplayString(status);
    }

    private string FormatStatusDisplayString(BaseStatus status) 
    {
        return string.Format("{0} / {1}", (int)status.CurrentCapacity, (int)status.MaxCapacity);
    }

    public void UpdateHealthBar() => UpdateBar(_healthBar, _healthValueText, _statusHandler.Health);

    public void UpdateStaminaBar() => UpdateBar(_staminaBar, _staminaValueText, _statusHandler.Stamina);

    public void UpdateManaBar() => UpdateBar(_manaBar, _manaValueText, _statusHandler.Mana);

    private void UpdateBar(Slider bar, TextMeshProUGUI uiText, BaseStatus status)
    {
        bar.value = status.CurrentCapacity; 
        uiText.text = FormatStatusDisplayString(status);
    }

    private string FormatStatusDisplayString(float current, float max) => string.Format("{0} / {1}", (int)current, (int)max);
}
