using System;
using UnityEngine;

public class PlayerStatusHandler : MonoBehaviour
{
    public static event Action OnDeath;

    #region references
    [SerializeField] private Player _player;
    private RegenStatus _health;
    private RegenStatus _stamina;
    private RegenStatus _mana;
    private float _regenDelay;
    private float _staminaRegenDelay;
    private float _healthRegenDelay;
    private float _manaRegenDelay;
    private bool _isDead;
    #endregion

    #region getters
    public RegenStatus Health { get => _health; }
    public RegenStatus Stamina { get => _stamina; }
    public RegenStatus Mana { get => _mana; }
    public bool IsDead { get => _isDead; set => _isDead = value; }
    #endregion

    #region Events
    public static event Action OnHealthChange;
    public static event Action OnStaminaChange;
    public static event Action OnManaChange;
    #endregion

    #region Awake
    private void Awake() 
    {    
        _regenDelay = _player.Config.misc.regenDelay;
        _health = MakeStatus(_player.Config.health);
        _stamina = MakeStatus(_player.Config.stamina);
        _mana = MakeStatus(_player.Config.mana);
    }

    private RegenStatus MakeStatus(StatusConfig config) => new RegenStatus(config.initialCapacity, config.regenPerSec, config.depletePerSec);
    #endregion

    #region Stamina consumption
    public void DepleteStamina()
    {
        _staminaRegenDelay = _regenDelay;
        _stamina.Deplete();
        OnStaminaChange?.Invoke();
    }

    public void UseStamina(float value)
    {
        _staminaRegenDelay = _regenDelay;
        _stamina.Take(value);
        OnStaminaChange?.Invoke();
    }
    #endregion

    #region Regenerate status
    public void Regenerate()
    {
        _healthRegenDelay = RegenerateStatus(_health, _healthRegenDelay, OnHealthChange);
        _staminaRegenDelay = RegenerateStatus(_stamina, _staminaRegenDelay, OnStaminaChange);
        _manaRegenDelay = RegenerateStatus(_mana, _manaRegenDelay, OnManaChange);
    }

    private float RegenerateStatus(RegenStatus status, float regenDelay, Action OnChange)
    {
        if (!status.IsFull() && regenDelay <= 0f)
        {
            status.Regenerate();
            OnChange?.Invoke();
        }
        else
        {
            regenDelay = CountDownTimer(regenDelay);
        }

        return regenDelay;
    }

    private float CountDownTimer(float value) => value <= 0f ? value : value - Time.deltaTime;
    #endregion

    public void Die() 
    {
        if (!_isDead)
        {
            _isDead = true;
            OnDeath?.Invoke();
        }
    }
}
