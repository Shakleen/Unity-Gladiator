using UnityEngine;
using UnityEngine.UI;

public class UIHandlerHUD : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider _staminaBar;
    [SerializeField] private Slider _manaBar;
    [SerializeField] private Player _player;

    private void Start()
    {
        InitSlider(_healthBar, _player.Health);
        InitSlider(_staminaBar, _player.Stamina);
        InitSlider(_manaBar, _player.Mana);
    }

    private void InitSlider(Slider slider, BaseStatus status)
    {
        slider.maxValue = status.MaxCapacity;
        slider.value = status.CurrentCapacity;
    }

    public void UpdateHealthBar(float value) { _healthBar.value = value; }

    public void UpdateStaminaBar(float value) { _staminaBar.value = value; }

    public void UpdateManaBar(float value) { _manaBar.value = value; }
}
