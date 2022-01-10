using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Editor tunable variables
    [Tooltip("Show debug log texts")]
    [SerializeField] private bool _showDebugText = false;

    [Tooltip("Maximum veloctiy the player can walk")]
    [SerializeField] [Range(0.5f, 5.0f)] private float _maxVelocityWalk = 1.0f;

    [Tooltip("Accelaration used to go towards maximum walk velocity")]
    [SerializeField] [Range(0.5f, 5.0f)] private float _accelarationWalk = 1.0f;

    [Tooltip("Deceleration used to go towards idle or zero velocity")]
    [SerializeField] [Range(0.5f, 5.0f)] private float _decelarationWalk = 1.0f;

    [Tooltip("Maximum veloctiy the player can run")]
    [SerializeField] [Range(0.5f, 5.0f)] private float _maxVelocityrun = 2.0f;

    [Tooltip("Accelaration used to go towards maximum run velocity")]
    [SerializeField] [Range(0.5f, 5.0f)] private float _accelarationRun = 2.0f;

    [Tooltip("Deceleration used to go towards max walk velocity")]
    [SerializeField] [Range(0.5f, 5.0f)] private float _decelarationRun = 1.0f;

    // Component references
    private Animator _animator;
    private CharacterController _controller;
    private InputActions _inputActions;
    private PlayerStateManager _stateManager;

    // Variables
    
    // State veriables
    private PlayerBaseState _currentState;
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

    // Movement variables
    [SerializeField] private bool _isInputActiveMovement = false;
    public bool IsInputActiveMovement { get { return _isInputActiveMovement; } }
    [SerializeField] private bool _isInputActiveRun = false;
    public bool IsInputActiveRun { get { return _isInputActiveRun; } }
    [SerializeField] private Vector2 _inputMovementVector = Vector2.zero;
    public Vector2 InputMovementVector { get { return _inputMovementVector; } }

    private void Awake() 
    {
        _animator = GetComponent<Animator>();    
        _controller = GetComponent<CharacterController>();
        _inputActions = new InputActions();
        _stateManager = new PlayerStateManager(this);
        _currentState = _stateManager.GetIdleState();
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

    private void Update() 
    {
        _currentState.ExecuteState();
        if (!_currentState.hasPrint) Debug.Log($"Player in state: {_currentState.GetName()}");
    }

    private void OnDisable() { _inputActions.Disable(); }
}
