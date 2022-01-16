using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    private const float THRESH = 1e-5f;
    [SerializeField] private Player _player;

    private int _idleComboTracker;
    private float _comboTimer;

    private void Start()
    {
        ResetComboTimer();
        ResetComboTracker();
    }

    private void Update()
    {
        if (_comboTimer > THRESH) 
            _comboTimer -= Time.deltaTime;
        else
            ResetComboTracker();
    }

    private void ResetComboTimer() => _comboTimer = _player.Config.misc.idleAttackComboMaxTime;

    private void ResetComboTracker() => _idleComboTracker = 0;

    private void IncrementComboTracker() => _idleComboTracker++;
}
