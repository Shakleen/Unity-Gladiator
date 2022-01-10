using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Show debug log texts")]
    [SerializeField] private bool _showDebugText = false;

    // Component references
    private Animator _animator;
    private CharacterController _controller;
    private InputActions _inputActions;

    // Variables

    // Movement variables
    [SerializeField] private bool _isInputActiveMovement = false;
    [SerializeField] private bool _isInputActiveRun = false;
    [SerializeField] private Vector2 _inputMovementVector = Vector2.zero;

    // Getters and setters
    public bool IsInputActiveMovement { get { return _isInputActiveMovement; } }
    public bool IsInputActiveRun { get { return _isInputActiveRun; } }
    public Vector2 InputMovementVector { get { return _inputMovementVector; } }

    private void Awake() 
    {
        _animator = GetComponent<Animator>();    
        _controller = GetComponent<CharacterController>();
        _inputActions = new InputActions();
    }

    private void OnEnable() 
    { 
        _inputActions.Enable(); 

        // Setup movement callbacks
        _inputActions.Player.Movement.started += InputCallbackMovement;
        _inputActions.Player.Movement.canceled += InputCallbackMovement;
        _inputActions.Player.Movement.performed += InputCallbackMovement;

        // Setup run callbacks
        _inputActions.Player.Run.started += InputCallbackRun;
        _inputActions.Player.Run.canceled += InputCallbackRun;
    }

    private void InputCallbackMovement(InputAction.CallbackContext context)
    {
        _inputMovementVector = context.ReadValue<Vector2>();
        _isInputActiveMovement = _inputMovementVector != Vector2.zero;
    }

    private void InputCallbackRun(InputAction.CallbackContext context)
    {
        _isInputActiveRun = context.ReadValueAsButton();
    }

    private void OnDisable() { _inputActions.Disable(); }
}
