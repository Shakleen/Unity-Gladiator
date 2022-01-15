using UnityEngine;

public class PlayerStatusHandler : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private UIHandlerHUD _hud;
    private RegenStatus _health;
    private RegenStatus _stamina;
    private RegenStatus _mana;

    public RegenStatus Health { get { return _health; } }
    public RegenStatus Stamina { get { return _stamina; } }
    public RegenStatus Mana { get { return _mana; } }

    private void Awake() 
    {    
        _health = new RegenStatus(
            _player.Config.StartingHealth, 
            _player.Config.HealthRegenPerSec, 
            0
        );
        _stamina = new RegenStatus(
            _player.Config.StartingStamina, 
            _player.Config.StaminaRegenPerSec, 
            _player.Config.StaminaDepletePerSec
        );
        _mana = new RegenStatus(
            _player.Config.StartingMana, 
            _player.Config.ManaRegenPerSec, 
            _player.Config.ManaDepletePerSec
        );
    }

    public void DepleteStamina()
    {
        _stamina.Deplete();
        _hud.UpdateStaminaBar();
    }

    public void UseStamina(float value)
    {
        _stamina.Take(value);
        _hud.UpdateStaminaBar();
    }

    public bool HasSufficientStamina() { return _stamina.CurrentCapacity > (_stamina.MaxCapacity / 5); }

    public void Regenerate()
    {
        _health.Regenerate();
        _hud.UpdateHealthBar();
        
        _stamina.Regenerate();
        _hud.UpdateStaminaBar();

        _mana.Regenerate();
        _hud.UpdateManaBar();
    }
}
