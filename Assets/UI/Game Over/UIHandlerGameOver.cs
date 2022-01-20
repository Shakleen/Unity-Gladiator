using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIHandlerGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _waveText;

    public void ShowGameOverDetails(int score, int waves)
    {
        _scoreText.text = score.ToString();
        _waveText.text = waves.ToString("D2");
    }

    public void MainMenuButton()
    {
        // TODO: Go to main menu
    }

    public void ReplayButton() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
