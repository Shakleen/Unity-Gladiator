using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIHandlerGameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private SessionData _session;

    private void OnEnable() => Cursor.lockState = CursorLockMode.None;

    private void OnDisable() => Cursor.lockState = CursorLockMode.Locked;

    public void SetWaveText() => _waveText.text = _session.Wave.ToString("D2");

    public void SetScoreText() => _scoreText.text = _session.Score.ToString();

    public void MainMenuButton() => SceneManager.LoadScene(0);

    public void ReplayButton() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
