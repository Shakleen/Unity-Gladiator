using UnityEngine;

public class Player : MonoBehaviour
{
    #region Component references
    [SerializeField] private PlayerMovementHandler _movementController;
    [SerializeField] private PlayerInputHandler _inputHandler;
    [SerializeField] private PlayerAnimationHandler _animationHandler;
    [SerializeField] private PlayerStatusHandler _statusHandler;
    [SerializeField] private PlayerAttackHandler _attackHandler;
    [SerializeField] private PlayerStateEnum _currentStateType;
    [SerializeField] private ConfigHandler _config;
    private PlayerStateMachine _stateMachine;
    #endregion


    #region Getters and setter
    public PlayerMovementHandler MovementHandler { get => _movementController; }
    public PlayerInputHandler InputHandler { get => _inputHandler; }
    public PlayerAnimationHandler AnimatorHandler { get => _animationHandler; }
    public PlayerStatusHandler StatusHandler { get => _statusHandler; }
    public PlayerAttackHandler AttackHandler { get => _attackHandler; }
    public PlayerStateEnum CurrentState { get => _currentStateType; set => _currentStateType = value; }
    public ConfigHandler Config { get => _config; }
    #endregion
    
    private void Awake() 
    { 
        Cursor.lockState = CursorLockMode.Locked; 
        _stateMachine = new PlayerStateMachine(this); 
    }

    private void Update()
    {
        _stateMachine.ExecuteState();
        _movementController.MoveCharacter();
        _statusHandler.Regenerate();
        _attackHandler.DecreaseAttackComboTimer();
    }
}
