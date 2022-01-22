using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerStatusHandler : MonoBehaviour
{
    public static event Action OnDeath;

    #region references
    private Player _player;
    public RegenStatus Health { get; private set; }
    public RegenStatus Stamina { get; private set; }
    private float _regenDelay;
    private float _staminaRegenDelay;
    private float _healthRegenDelay;
    private float _manaRegenDelay;
    private bool _isDead;
    #endregion

    #region getters
    public bool IsDead { get => _isDead; set => _isDead = value; }
    #endregion

    #region Events
    public static event Action<BaseStatus> OnHealthChange;
    public static event Action<BaseStatus> OnStaminaChange;
    #endregion
    
    private void Awake() 
    {    
        _player = GetComponent<Player>();
        _regenDelay = _player.Config.misc.regenDelay;
        Health = MakeStatus(_player.Config.health);
        Stamina = MakeStatus(_player.Config.stamina);
    }

    private RegenStatus MakeStatus(StatusConfig config) => new RegenStatus(config.initialCapacity, config.regenPerSec, config.depletePerSec);

    private void Start() 
    {
        OnHealthChange?.Invoke(Health);
        OnStaminaChange?.Invoke(Stamina);
    }

    #region Take Damage
    public void TakeDamage(float damage)
    {
        Health.Take(damage);
        OnHealthChange?.Invoke(Health);
    }
    #endregion

    #region Stamina consumption
    public void DepleteStamina()
    {
        _staminaRegenDelay = _regenDelay;
        Stamina.Deplete();
        OnStaminaChange?.Invoke(Stamina);
    }

    public void UseStamina(float value)
    {
        _staminaRegenDelay = _regenDelay;
        Stamina.Take(value);
        OnStaminaChange?.Invoke(Stamina);
    }
    #endregion

    #region Regenerate status
    public void Regenerate()
    {
        _healthRegenDelay = RegenerateStatus(Health, _healthRegenDelay, OnHealthChange);
        _staminaRegenDelay = RegenerateStatus(Stamina, _staminaRegenDelay, OnStaminaChange);
    }

    private float RegenerateStatus(RegenStatus status, float regenDelay, Action<BaseStatus> OnChange)
    {
        if (!status.IsFull() && regenDelay <= 0f)
        {
            status.Regenerate();
            OnChange?.Invoke(status);
        }
        else
            regenDelay = CountDownTimer(regenDelay);

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
