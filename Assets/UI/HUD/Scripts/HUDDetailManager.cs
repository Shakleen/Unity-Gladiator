using UnityEngine;
using TMPro;

public class HUDDetailManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _waveNoText;
    [SerializeField] private TextMeshProUGUI _timeRemainingText;
    [SerializeField] private SessionData _session;

    public void SetScoreText() => _scoreText.text = $"{_session.Score}";

    public void SetWaveNumberText() => _waveNoText.text = _session.Wave.ToString("D2");

    public void SetTimeRemainingText() 
    {
        int minutes = (int) (_session.TimeRemaining / 60);
        int seconds = (int) (_session.TimeRemaining % 60);
        _timeRemainingText.text = $"{minutes:D2}:{seconds:D2}";
    }
}
