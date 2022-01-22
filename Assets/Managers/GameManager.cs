using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Events
    public static event Action OnWaveNoChange;
    public static event Action OnTimerChange;
    public static event Action OnScoreChange;
    public static event Action OnGameOver;
    #endregion

    #region Serialize Fields
    [SerializeField] private int _waveTimeLimit = 180;
    [SerializeField] private int _nextWaveLoadTime = 30;
    #endregion

    private int _waveNo, _score, _waveTimer;
    private IEnumerator _timerCoroutine;

    #region Getters
    public int WaveNo { get => _waveNo; }
    public int Score { get => _score; }
    public int WaveTimeRemaining { get => _waveTimer; }
    #endregion

    private void OnEnable() => PlayerStatusHandler.OnDeath += GameOver;

    private void OnDisable() => PlayerStatusHandler.OnDeath -= GameOver;

    private void Start() 
    {
        SetWaveTimer(_waveTimeLimit);  
        IncrementWaveNumber();
        AddScore(0);
    }

    private void SetWaveTimer(int time) 
    {
        if (_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);

        _waveTimer = time;
        _timerCoroutine = CountDownWaveTimer();
        StartCoroutine(_timerCoroutine);
    }

    private IEnumerator CountDownWaveTimer()
    {
        while (_waveTimer > 0)
        {
            _waveTimer--;
            OnTimerChange?.Invoke();
            yield return new WaitForSeconds(1);
        }
    }

    public void EndWave() => StartCoroutine(LoadNextWave());

    private IEnumerator LoadNextWave()
    {
        SetWaveTimer(_nextWaveLoadTime);
        yield return new WaitForSeconds(_nextWaveLoadTime);
        IncrementWaveNumber();
        SetWaveTimer(_waveTimeLimit);
    }

    private void IncrementWaveNumber()
    {
        _waveNo++;
        OnWaveNoChange?.Invoke();
    }

    public void AddScore(int value)
    {
        _score += value;
        OnScoreChange?.Invoke();
    }

    public void GameOver() => OnGameOver?.Invoke();
}
