using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Events
    public static event Action OnWaveNoChange;
    public static event Action OnTimerChange;
    public static event Action OnScoreChange;
    #endregion

    [SerializeField] private int _waveTimeLimit = 180;

    private int _waveNo, _score, _waveTimer;

    #region Getters
    public int WaveNo { get => _waveNo; }
    public int Score { get => _score; }
    public int WaveTimeRemaining { get => _waveTimer; }
    #endregion

    private void Start() 
    {
        InitializeWaveTimer();  
        IncrementWaveNumber();
        AddScore(0);
    }

    private void InitializeWaveTimer() 
    {
        _waveTimer = _waveTimeLimit;
        StartCoroutine(CountDownWaveTimer());
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
}
