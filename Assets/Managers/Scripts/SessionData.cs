using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SessionData", menuName = "Gladiator/General/Session Data", order = 0)]
public class SessionData : ScriptableObject 
{
    [Tooltip("Seconds within which enemies must be cleared out.")] [SerializeField] private int _waveTime = 180;
    [Tooltip("Seconds between the end and starting of a wave")] [SerializeField] private int _prepareTime = 30;
    public int PrepareTime { get => _prepareTime; }
    public int Wave { get; private set; }
    public int Score { get; private set; }
    public int TimeRemaining { get; private set; }

    private void OnEnable() => TimeRemaining = Wave = Score = 0;

    public void BeginWave()
    {
        Wave++;
        TimeRemaining = _waveTime;
    }

    public void AddScore(int value) => Score += value;

    public void SetTimeToPrepareTime() => TimeRemaining = _prepareTime;

    public void DecrementTime() => TimeRemaining--;
}