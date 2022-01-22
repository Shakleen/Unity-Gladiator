using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIHandlerGameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _waveText;

    private void Awake() 
    {
        GameManager.OnWaveNoChange += SetWaveText;
        GameManager.OnScoreChange += SetScoreText;
    }

    private void OnEnable() => Cursor.lockState = CursorLockMode.None;

    private void OnDisable() => Cursor.lockState = CursorLockMode.Locked;

    private void OnDestroy() 
    {
        Cursor.lockState = CursorLockMode.None;
        GameManager.OnWaveNoChange -= SetWaveText;
        GameManager.OnScoreChange -= SetScoreText;
    }

    private void SetWaveText(int value) => _waveText.text = value.ToString("D2");

    private void SetScoreText(int value) => _scoreText.text = value.ToString();

    public void MainMenuButton() => SceneManager.LoadScene(0);

    public void ReplayButton() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
