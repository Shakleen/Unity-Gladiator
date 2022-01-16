using UnityEngine;

public class PlayerStatusHandler : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private UIHandlerHUD _hud;

    private RegenStatus _health;
    private RegenStatus _stamina;
    private RegenStatus _mana;
    private float _regenDelay;
    private float _staminaRegenDelay;
    private float _healthRegenDelay;
    private float _manaRegenDelay;

    public RegenStatus Health { get { return _health; } }
    public RegenStatus Stamina { get { return _stamina; } }
    public RegenStatus Mana { get { return _mana; } }

    private void Awake() 
    {    
        _regenDelay = _player.Config.misc.regenDelay;
        _health = new RegenStatus(
            _player.Config.health.initialCapacity, 
            _player.Config.health.regenPerSec, 
            _player.Config.health.depletePerSec
        );
        _stamina = new RegenStatus(
            _player.Config.stamina.initialCapacity, 
            _player.Config.stamina.regenPerSec, 
            _player.Config.stamina.depletePerSec
        );
        _mana = new RegenStatus(
            _player.Config.mana.initialCapacity, 
            _player.Config.mana.regenPerSec, 
            _player.Config.mana.depletePerSec
        );
    }

    private void Start()
    {
        _hud.UpdateHealthBar();
        _hud.UpdateStaminaBar();
        _hud.UpdateManaBar();
    }

    public void DepleteStamina()
    {
        _staminaRegenDelay = _regenDelay;
        _stamina.Deplete();
        _hud.UpdateStaminaBar();
    }

    public void UseStamina(float value)
    {
        _staminaRegenDelay = _regenDelay;
        _stamina.Take(value);
        _hud.UpdateStaminaBar();
    }

    public bool HasSufficientStamina() { return _stamina.CurrentCapacity > (_stamina.MaxCapacity / 5); }

    public void Regenerate()
    {
        if (!_health.IsFull() && _healthRegenDelay <= 0f)
        {
            _health.Regenerate();
            _hud.UpdateHealthBar();
        }
        
        if (!_stamina.IsFull() && _staminaRegenDelay <= 0f)
        {
            _stamina.Regenerate();
            _hud.UpdateStaminaBar();
        }

        if (!_mana.IsFull() && _manaRegenDelay <= 0f)
        {
            _mana.Regenerate();
            _hud.UpdateManaBar();
        }

        _healthRegenDelay = DecreaseDelayTimer(_healthRegenDelay);
        _staminaRegenDelay = DecreaseDelayTimer(_staminaRegenDelay);
        _manaRegenDelay = DecreaseDelayTimer(_manaRegenDelay);
    }

    private float DecreaseDelayTimer(float value) { return value <= 0f ? value : value - Time.deltaTime; }
}
