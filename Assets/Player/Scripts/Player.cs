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


    // =================================================================================================================================================
    //                                                              Getters and Setters
    // =================================================================================================================================================
    public PlayerMovementHandler MovementHandler { get { return _movementController; } }
    public PlayerInputHandler InputHandler { get { return _inputHandler; } }
    public PlayerAnimationHandler AnimatorHandler { get { return _animationHandler; } }
    public PlayerConfig Config { get { return _config; } }

    // =================================================================================================================================================
    //                                                                  Functions
    // =================================================================================================================================================
    private void Awake() 
    { 
        _stateMachine = new PlayerStateMachine(this); 
        Cursor.lockState = CursorLockMode.Locked; 
    }

    private void Update()
    {
        _currentStateType = _stateMachine.GetCurrentStateType();
        _stateMachine.ExecuteState();
        _movementController.ApplyGravity();
    }

}
