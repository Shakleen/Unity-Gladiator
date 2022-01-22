using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class UIHandlerPauseMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private GameManager _gameManager;

    private int _score, _wave;

    private void Awake() 
    {
        GameManager.OnWaveNoChange += SetWaveText;
        GameManager.OnScoreChange += SetScoreText;
    }

    private void OnDestroy() 
    {
        GameManager.OnWaveNoChange -= SetWaveText;
        GameManager.OnScoreChange -= SetScoreText;
    }

    private void Start() 
    {
        SetWaveText();    
        SetScoreText();
    }

    private void SetWaveText() 
    {
        _wave = _gameManager.WaveNo;
        _waveText.text = _wave.ToString("D2");
    }

    private void SetScoreText() 
    {
        _score = _gameManager.Score;
        _scoreText.text = _score.ToString();
    }

    public void MainMenuButton() => SceneManager.LoadScene(0);

    public void ReplayButton() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
