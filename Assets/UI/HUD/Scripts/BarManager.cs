using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarManager : MonoBehaviour
{
    [SerializeField] private Slider _rightSlider;
    [SerializeField] private Slider _leftSlider;
    [SerializeField] private TextMeshProUGUI _value;

    private bool _initialized = false;
    
    public void UpdateBar(BaseStatus status)
    {
        if (!_initialized)
        {
            _initialized = true;
            InitializeBar(status);
        }

        _leftSlider.value = status.CurrentCapacity;
        _rightSlider.value = status.CurrentCapacity;

        if (_value != null)
            _value.text = string.Format("{0} / {1}", (int)status.CurrentCapacity, (int)status.MaxCapacity);
    }

    public void InitializeBar(BaseStatus status)
    {
        _leftSlider.maxValue = status.MaxCapacity;
        _rightSlider.maxValue = status.MaxCapacity;
    }
}
