using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarManager : MonoBehaviour
{
    [SerializeField] private Slider _rightSlider;
    [SerializeField] private Slider _leftSlider;
    [SerializeField] private TextMeshProUGUI _value;

    private bool _initialized = false;

    public void UpdateBarValue(Status status)
    {
        if (!_initialized)
            InitializeMaxValue(status);

        UpdateValue(status);
    }

    private void UpdateValue(Status status)
    {
        _leftSlider.value = status.currentCapacity;
        _rightSlider.value = status.currentCapacity;

        if (_value != null)
            _value.text = string.Format("{0} / {1}", (int)status.currentCapacity, (int)status.maxCapacity);
    }

    private void InitializeMaxValue(Status status)
    {
        _initialized = true;
        _leftSlider.maxValue = status.maxCapacity;
        _rightSlider.maxValue = status.maxCapacity;
    }
}
