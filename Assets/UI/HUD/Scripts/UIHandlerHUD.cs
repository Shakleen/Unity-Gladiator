using UnityEngine;

public class UIHandlerHUD : MonoBehaviour
{
    [SerializeField] private HUDDetailManager _details;

    private void Awake() 
    {
        GameManager.OnTimerChange += UpdateWaveTimeRemaining;
        GameManager.OnWaveNoChange += UpdateWaveNo;
        GameManager.OnScoreChange += UpdateScore;
    }

    private void OnEnable() => Cursor.lockState = CursorLockMode.Locked;

    private void OnDisable() => Cursor.lockState = CursorLockMode.None;

    private void OnDestroy() 
    {
        GameManager.OnTimerChange -= UpdateWaveTimeRemaining;
        GameManager.OnWaveNoChange -= UpdateWaveNo;
        GameManager.OnScoreChange -= UpdateScore;
    }

    #region Event callbacks
    private void UpdateWaveTimeRemaining(int value) => _details.SetTimeRemainingText(value);
    private void UpdateWaveNo(int value) => _details.SetWaveNumberText(value);
    private void UpdateScore(int value) => _details.SetScoreText(value);
    #endregion
}
