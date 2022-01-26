using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIHandlerHUD _hudScreen;
    [SerializeField] private UIHandlerGameOver _gameOverScreen;
    [SerializeField] private UIHandlerPauseMenu _pauseScreen;

    private bool _isPaused;

    public void HandleOnPause() 
    {
        _isPaused = !_isPaused;
        SetScreenActive(pause: _isPaused, hud: !_isPaused);
    }

    public void HandleOnGameOver() => SetScreenActive(gameover: true);

    private void Start() => SetScreenActive(hud: true);

    private void SetScreenActive(bool hud = false, bool pause = false, bool gameover = false)
    {
        _hudScreen.gameObject.SetActive(hud);
        _pauseScreen.gameObject.SetActive(pause);
        _gameOverScreen.gameObject.SetActive(gameover);
        Time.timeScale = hud ? 1.0f : 0.05f;
    }
}
