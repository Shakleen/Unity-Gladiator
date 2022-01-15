using UnityEngine;

public class Player : MonoBehaviour
{
    // =================================================================================================================================================
    //                                                          References and Variables
    // =================================================================================================================================================
    [SerializeField] private PlayerMovementHandler _movementController;
    [SerializeField] private PlayerInputHandler _inputHandler;
    [SerializeField] private PlayerAnimationHandler _animationHandler;
    [SerializeField] private PlayerConfig _config;
    [SerializeField] private PlayerStateType _currentStateType;
    private PlayerStateMachine _stateMachine;
    private RegenStatus _health;
    private RegenStatus _stamina;
    private RegenStatus _mana;


    // =================================================================================================================================================
    //                                                              Getters and Setters
    // =================================================================================================================================================
    public PlayerMovementHandler MovementHandler { get { return _movementController; } }
    public PlayerInputHandler InputHandler { get { return _inputHandler; } }
    public PlayerAnimationHandler AnimatorHandler { get { return _animationHandler; } }
    public PlayerConfig Config { get { return _config; } }
    public RegenStatus Health { get { return _health; } }
    public RegenStatus Stamina { get { return _stamina; } }
    public RegenStatus Mana { get { return _mana; } }

    // =================================================================================================================================================
    //                                                                  Functions
    // =================================================================================================================================================
    private void Awake() 
    { 
        Cursor.lockState = CursorLockMode.Locked; 
        _stateMachine = new PlayerStateMachine(this); 
        _health = new RegenStatus(_config.StartingHealth, _config.HealthRegenPerSec, 0);
        _stamina = new RegenStatus(_config.StartingStamina, _config.StaminaRegenPerSec, _config.StaminaDepletePerSec);
        _mana = new RegenStatus(_config.StartingMana, _config.ManaRegenPerSec, _config.ManaDepletePerSec);
    }

    private void Update()
    {
        _currentStateType = _stateMachine.GetCurrentStateType();
        _stateMachine.ExecuteState();
        _movementController.ApplyGravity();
    }

}
