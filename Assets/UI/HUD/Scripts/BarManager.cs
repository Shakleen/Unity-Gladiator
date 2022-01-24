using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarManager : MonoBehaviour
{
    [SerializeField] private Slider _rightSlider;
    [SerializeField] private Slider _leftSlider;
    [SerializeField] private TextMeshProUGUI _value;
    [SerializeField] private StatusConfig _status;

    private bool _initialized = false;

    public void UpdateBarValue()
    {
        if (!_initialized)
            InitializeMaxValue();

        UpdateValue();
    }

    private void UpdateValue()
    {
        _leftSlider.value = _status.currentCapacity;
        _rightSlider.value = _status.currentCapacity;

        if (_value != null)
            _value.text = string.Format("{0} / {1}", (int)_status.currentCapacity, (int)_status.maxCapacity);
    }

    private void InitializeMaxValue()
    {
        _initialized = true;
        _leftSlider.maxValue = _status.maxCapacity;
        _rightSlider.maxValue = _status.maxCapacity;
    }
}
