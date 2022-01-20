using UnityEngine;
using TMPro;

public class HUDDetailManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _waveNoText;
    [SerializeField] private TextMeshProUGUI _timeRemainingText;

    public void SetScoreText(int score) => _scoreText.text = $"{score}";

    public void SetWaveNumberText(int wave) => _waveNoText.text = wave.ToString("D2");

    public void SetTimeRemainingText(int remainingTime) 
    {
        int minutes = (int) (remainingTime / 60);
        int seconds = (int) (remainingTime % 60);
        _timeRemainingText.text = $"{minutes:D2}:{seconds:D2}";
    }
}
