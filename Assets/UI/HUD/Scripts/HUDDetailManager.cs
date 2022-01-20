using UnityEngine;
using TMPro;

public class HUDDetailManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _waveNoText;
    [SerializeField] private TextMeshProUGUI _timerText;

    public void SetScoreText(int score) => _scoreText.text = $"{score}";

    public void SetWaveNumberText(int wave) => _waveNoText.text = wave.ToString("D2");

    public void SetTimerText(int minutes, int seconds) => _timerText.text = $"{minutes:D2} : {seconds:D2}";
}
