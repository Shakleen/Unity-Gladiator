using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerStatusHandler : MonoBehaviour
{
    public static event Action OnDeath;
    [SerializeField] private GameEvent _onHealthChange;
    [SerializeField] private GameEvent _onStaminaChange;

    #region references
    private Player _player;
    private float _staminaRegenDelay, _healthRegenDelay;
    private bool _isDead;
    #endregion

    #region getters
    public bool IsDead { get => _isDead; set => _isDead = value; }
    #endregion
    
    private void Awake() => _player = GetComponent<Player>();

    private void Start() 
    {
        _onHealthChange.Raise();
        _onStaminaChange.Raise();    
    }

    public void TakeDamage(float damage)
    {
        _player.Config.health.Take(damage);
        _onHealthChange.Raise();
    }

    #region Stamina consumption
    public void DepleteStamina()
    {
        ResetStaminaRegenDelay();
        _player.Config.stamina.Deplete();
        _onStaminaChange.Raise();
    }

    public void UseStamina(float value)
    {
        ResetStaminaRegenDelay();
        _player.Config.stamina.Take(value);
        _onStaminaChange.Raise();
    }

    private void ResetStaminaRegenDelay() => _staminaRegenDelay = _player.Config.stamina.regenDelay;
    #endregion

    #region Regenerate status
    public void Regenerate()
    {
        _healthRegenDelay = RegenerateStatus(
            _player.Config.health, 
            _healthRegenDelay, 
            _onHealthChange
        );
        _staminaRegenDelay = RegenerateStatus(
            _player.Config.stamina, 
            _staminaRegenDelay, 
            _onStaminaChange
        );
    }

    private float RegenerateStatus(StatusConfig status, float regenDelay, GameEvent OnChange)
    {
        if (!status.IsFull() && regenDelay <= 0f)
        {
            status.Regenerate();
            OnChange.Raise();
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
