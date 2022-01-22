using UnityEngine;

public class UIHandlerHUD : MonoBehaviour
{
    [SerializeField] private BarManager _healthBar;
    [SerializeField] private BarManager _staminaBar;
    [SerializeField] private HUDDetailManager _details;
    [SerializeField] private GameManager _gameManager;

    private void Awake() 
    {
        PlayerStatusHandler.OnHealthChange += UpdateHealthBar;
        PlayerStatusHandler.OnStaminaChange += UpdateStaminaBar;
        GameManager.OnTimerChange += UpdateWaveTimeRemaining;
        GameManager.OnWaveNoChange += UpdateWaveNo;
        GameManager.OnScoreChange += UpdateScore;
    }

    private void OnEnable() => Cursor.lockState = CursorLockMode.Locked;

    private void OnDisable() => Cursor.lockState = CursorLockMode.None;

    private void OnDestroy() 
    {
        PlayerStatusHandler.OnHealthChange -= UpdateHealthBar;
        PlayerStatusHandler.OnStaminaChange -= UpdateStaminaBar;
        GameManager.OnTimerChange -= UpdateWaveTimeRemaining;
        GameManager.OnWaveNoChange -= UpdateWaveNo;
        GameManager.OnScoreChange -= UpdateScore;
    }

    #region Event callbacks
    public void UpdateHealthBar(BaseStatus status) => _healthBar.UpdateBar(status);
    public void UpdateStaminaBar(BaseStatus status) => _staminaBar.UpdateBar(status);
    private void UpdateWaveTimeRemaining(int value) => _details.SetTimeRemainingText(value);
    private void UpdateWaveNo(int value) => _details.SetWaveNumberText(value);
    private void UpdateScore(int value) => _details.SetScoreText(value);
    #endregion
}
