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
            _player.Config.health.startingCapacity, 
            _player.Config.health.regenPerSec, 
            _player.Config.health.depletePerSec
        );
        _stamina = new RegenStatus(
            _player.Config.stamina.startingCapacity, 
            _player.Config.stamina.regenPerSec, 
            _player.Config.stamina.depletePerSec
        );
        _mana = new RegenStatus(
            _player.Config.mana.startingCapacity, 
            _player.Config.mana.regenPerSec, 
            _player.Config.mana.depletePerSec
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
