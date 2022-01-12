using UnityEngine;

[RequireComponent(typeof(PlayerMovementController), typeof(PlayerInputHandler), typeof(PlayerAnimationHandler))]
public class Player : MonoBehaviour
{
    // =================================================================================================================================================
    //                                                          References and Variables
    // =================================================================================================================================================
    private PlayerMovementController _movementController;
    private PlayerInputHandler _inputHandler;
    private PlayerAnimationHandler _animationHandler;


    // =================================================================================================================================================
    //                                                              Getters and Setters
    // =================================================================================================================================================
    public PlayerMovementController MovementHandler { get { return _movementController; } }
    public PlayerInputHandler InputHandler { get { return _inputHandler; } }
    public PlayerAnimationHandler AnimatorHandler { get { return _animationHandler; } }

    // =================================================================================================================================================
    //                                                                  Functions
    // =================================================================================================================================================
    private void Awake() 
    {
        _movementController = GetComponent<PlayerMovementController>();
        _inputHandler = GetComponent<PlayerInputHandler>();
        _animationHandler = GetComponent<PlayerAnimationHandler>();
    }

    private void Start() { Cursor.lockState = CursorLockMode.Locked; }

}
