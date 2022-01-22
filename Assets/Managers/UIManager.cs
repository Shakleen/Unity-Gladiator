using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIHandlerHUD _hudScreen;
    [SerializeField] private UIHandlerGameOver _gameOverScreen;
    [SerializeField] private UIHandlerPauseMenu _pauseScreen;

    private void OnEnable() 
    {
        PlayerInputHandler.OnPausePressed += HandleOnPause;
        GameManager.OnGameOver += HandleOnGameOver;
    }

    private void OnDisable() 
    {
        PlayerInputHandler.OnPausePressed -= HandleOnPause;
        GameManager.OnGameOver -= HandleOnGameOver;
    }

    private void HandleOnPause(bool isPaused) => SetScreenActive(pause: isPaused, hud: !isPaused);

    private void HandleOnGameOver() => SetScreenActive(gameover: true);

    private void Start() => SetScreenActive(hud: true);

    public void SetScreenActive(bool hud = false, bool pause = false, bool gameover = false)
    {
        _hudScreen.gameObject.SetActive(hud);
        _pauseScreen.gameObject.SetActive(pause);
        _gameOverScreen.gameObject.SetActive(gameover);
        Time.timeScale = hud ? 1.0f : 0.05f;
    }
}
