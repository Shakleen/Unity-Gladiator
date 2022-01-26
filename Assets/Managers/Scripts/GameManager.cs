using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Events
    [SerializeField] private GameEvent _onWaveNoChange;
    [SerializeField] private GameEvent _onTimerChange;
    [SerializeField] private GameEvent _onScoreChange;
    [SerializeField] private GameEvent _onGameOver;
    [SerializeField] private SessionData _session;
    #endregion

    private IEnumerator _timerCoroutine;
    private WaitForSeconds _timerDelay, _prepareTime;

    private void Awake() 
    {
        _timerDelay = new WaitForSeconds(1f);
        _prepareTime = new WaitForSeconds(_session.PrepareTime);
    }

    private void OnEnable()
    {
        PlayerStatusHandler.OnDeath += GameOver;
        EnemySpawner.WaveEnd += EndWave;
    }

    private void OnDisable()
    {
        PlayerStatusHandler.OnDeath -= GameOver;
        EnemySpawner.WaveEnd -= EndWave;
    }

    private void Start() 
    {
        _session.BeginWave();
        StartWaveTimer();
        _onWaveNoChange.Raise();
        _onTimerChange.Raise();
        _onScoreChange.Raise();
    }

    #region Timer count down logic
    private void StartWaveTimer() 
    {
        if (_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);
        
        _timerCoroutine = TimerCoroutine();
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
    private void EndWave() => StartCoroutine(LoadNextWave());

    private IEnumerator LoadNextWave()
    {
        _session.SetTimeToPrepareTime();
        StartWaveTimer();

        yield return _prepareTime;
        
        _session.BeginWave();
        _onWaveNoChange.Raise();
        StartWaveTimer();
    }
    #endregion

    public void AddScore()
    {
        _session.AddScore(10);
        _onScoreChange.Raise();
    }

    public void GameOver() => _onGameOver.Raise();
}
