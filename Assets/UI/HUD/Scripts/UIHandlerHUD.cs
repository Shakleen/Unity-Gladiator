using UnityEngine;

public class UIHandlerHUD : MonoBehaviour
{
    [SerializeField] private BarManager _healthBar;
    [SerializeField] private BarManager _staminaBar;
    [SerializeField] private HUDDetailManager _details;
    [SerializeField] private PlayerStatusHandler _statusHandler;
    [SerializeField] private GameManager _gameManager;

    private void OnEnable() 
    {
        PlayerStatusHandler.OnHealthChange += UpdateHealthBar;
        PlayerStatusHandler.OnStaminaChange += UpdateStaminaBar;
        GameManager.OnTimerChange += UpdateWaveTimeRemaining;
        GameManager.OnWaveNoChange += UpdateWaveNo;
        GameManager.OnScoreChange += UpdateScore;
    }

    private void OnDisable() 
    {
        PlayerStatusHandler.OnHealthChange -= UpdateHealthBar;
        PlayerStatusHandler.OnStaminaChange -= UpdateStaminaBar;
        GameManager.OnTimerChange -= UpdateWaveTimeRemaining;
        GameManager.OnWaveNoChange -= UpdateWaveNo;
        GameManager.OnScoreChange -= UpdateScore;
    }

    #region Event callbacks
    public void UpdateHealthBar() => _healthBar.UpdateBar(_statusHandler.Health);
    public void UpdateStaminaBar() => _staminaBar.UpdateBar(_statusHandler.Stamina);
    private void UpdateWaveTimeRemaining() => _details.SetTimeRemainingText(_gameManager.WaveTimeRemaining);
    private void UpdateWaveNo() => _details.SetWaveNumberText(_gameManager.WaveNo);
    private void UpdateScore() => _details.SetScoreText(_gameManager.Score);
    #endregion

    private void Start()
    {
        _healthBar.InitializeBar(_statusHandler.Health);
        _staminaBar.InitializeBar(_statusHandler.Stamina);
    }
}
