using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerStatusHandler : MonoBehaviour
{
    public static event Action OnDeath;
    [SerializeField] private GameEvent_Status _onHealthChange;
    [SerializeField] private GameEvent_Status _onStaminaChange;

    #region references
    private Player _player;
    private float _staminaRegenDelay, _healthRegenDelay;
    private bool _isDead;
    #endregion

    #region getters
    public bool IsDead { get => _isDead; set => _isDead = value; }
    public Status Health { get; private set; }
    public Status Stamina { get; private set; }
    #endregion
    
    private void Awake() => _player = GetComponent<Player>();

    private void Start() 
    {
        Health = new Status(_player.Config.health);
        Stamina = new Status(_player.Config.stamina);
        _onHealthChange.Raise(Health);
        _onStaminaChange.Raise(Stamina);
    }

    public void TakeDamage(float damage)
    {
        Health.Take(damage);
        _onHealthChange.Raise(Health);
    }

    #region Stamina consumption
    public void DepleteStamina()
    {
        ResetStaminaRegenDelay();
        Stamina.Deplete();
        _onStaminaChange.Raise(Stamina);
    }

    public void UseStamina(float value)
    {
        ResetStaminaRegenDelay();
        Stamina.Take(value);
        _onStaminaChange.Raise(Stamina);
    }

    private void ResetStaminaRegenDelay() => _staminaRegenDelay = _player.Config.stamina.regenDelay;
    #endregion

    #region Regenerate status
    public void Regenerate()
    {
        _healthRegenDelay = RegenerateStatus(
            Health, 
            _healthRegenDelay, 
            _onHealthChange
        );
        _staminaRegenDelay = RegenerateStatus(
            Stamina, 
            _staminaRegenDelay, 
            _onStaminaChange
        );
    }

    private float RegenerateStatus(Status status, float regenDelay, GameEvent_Status OnChange)
    {
        if (!status.IsFull() && regenDelay <= 0f)
        {
            status.Regenerate();
            OnChange.Raise(status);
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
