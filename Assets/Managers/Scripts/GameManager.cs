using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Events
    [SerializeField] private GameEvent _onWaveNoChange;
    [SerializeField] private GameEvent _onTimerChange;
    [SerializeField] private GameEvent _onScoreChange;
    [SerializeField] private GameEvent _onWaveBegin;
    [SerializeField] private GameEvent _onGameOver;
    [SerializeField] private SessionData _session;
    #endregion

    private IEnumerator _timerCoroutine;
    private WaitForSeconds _timerDelay, _prepareTime;

    private void Awake() 
    {
        _timerDelay = new WaitForSeconds(1f);
        _prepareTime = new WaitForSeconds(_session.PrepareTime);
        _timerCoroutine = TimerCoroutine();
    }

    private void OnEnable()
    {
        PlayerStatusHandler.OnDeath += GameOver;
    }

    private void OnDisable()
    {
        PlayerStatusHandler.OnDeath -= GameOver;
    }

    private void Start() 
    {
        StartWave();
        _onTimerChange.Raise();
        _onScoreChange.Raise();
    }

    #region Timer count down logic
    private void StartWaveTimer() 
    {
        if (_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);
        
        StartCoroutine(_timerCoroutine);
    }

    private IEnumerator TimerCoroutine()
    {
        while (_session.TimeRemaining > 0)
        {
            _session.DecrementTime();
            _onTimerChange.Raise();
            yield return _timerDelay;
        }
    }
    #endregion

    #region Wave ending
    public void EndWave() => StartCoroutine(LoadNextWave());

    private IEnumerator LoadNextWave()
    {
        _session.SetTimeToPrepareTime();
        StartWaveTimer();
        yield return _prepareTime;
        StartWave();
    }
    #endregion

    private void StartWave()
    {
        _session.BeginWave();
        _onWaveNoChange.Raise();
        _onWaveBegin.Raise();
        StartWaveTimer();
    }

    public void AddScore()
    {
        _session.AddScore(10);
        _onScoreChange.Raise();
    }

    public void GameOver() => _onGameOver.Raise();
}
