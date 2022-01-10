using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Editor tunable variables
    [Tooltip("Show debug log texts")]
    [SerializeField] private bool _showDebugText = false;

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [Header("Walk Variables")]
    [Tooltip("Maximum forward veloctiy the player can walk")] [SerializeField] [Range(0.5f, 5.0f)] private float _maxForwardWalkVelocity = 1.0f;
    public float MaxForwardWalkVelocity { get { return _maxForwardWalkVelocity; } }
    [Tooltip("Accelaration used to go towards maximum walk velocity")] [SerializeField] [Range(0.5f, 5.0f)] private float _accelarationWalk = 2.0f;
    public float AccelarationWalk { get { return _accelarationWalk; } }
    [Tooltip("Deceleration used to go towards idle or zero velocity")] [SerializeField] [Range(0.5f, 5.0f)] private float _decelarationWalk = 2.0f;
    public float DecelarationWalk { get { return _decelarationWalk; } }
    // -------------------------------------------------------------------------------------------------------------------------------------------------

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [Header("Run Variables")]
    [Tooltip("Maximum veloctiy the player can run")] [SerializeField] [Range(0.5f, 5.0f)] private float _maxVelocityRun = 2.0f;
    public float MaxVelocityRun { get { return _maxVelocityRun; } }
    [Tooltip("Accelaration used to go towards maximum run velocity")] [SerializeField] [Range(0.5f, 5.0f)] private float _accelarationRun = 2.0f;
    public float AccelarationRun { get { return _accelarationRun; } }
    [Tooltip("Deceleration used to go towards max walk velocity")] [SerializeField] [Range(0.5f, 5.0f)] private float _decelarationRun = 1.0f;
    public float DecelarationRun { get { return _decelarationRun; } }
    // -------------------------------------------------------------------------------------------------------------------------------------------------

    // Component references
    private Animator _animator;
    public Animator Animator { get { return _animator; } }
    private CharacterController _controller;
    private InputActions _inputActions;
    private PlayerStateManager _stateManager;

    // Variables
    
    // State veriables
    private PlayerBaseState _currentState;
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

    // Animator hashes
    private int _animatorHashVelocityX;
    private int _animatorHashVelocityZ;
    private int _animatorHashIsRunning;
    private int _animatorHashIsDodging;
    public int AnimatorHashIsDodging { get { return _animatorHashIsDodging; } }

    // Movement variables
    [SerializeField] private Vector3 _currentMovementVelocity = Vector3.zero;
    public Vector3 CurrentMovementVelocity { get { return _currentMovementVelocity; } set { _currentMovementVelocity = value; } }
    public float CurrentMovementVelocityX 
    { 
        get { return _currentMovementVelocity.x; } 
        set 
        { 
            _currentMovementVelocity.x = value; 
            _animator.SetFloat(_animatorHashVelocityX, value);
        } 
    }
    public float CurrentMovementVelocityZ 
    { 
        get { return _currentMovementVelocity.z; } 
        set 
        { 
            _currentMovementVelocity.z = value; 
            _animator.SetFloat(_animatorHashVelocityZ, value);
        } 
    }
    private bool _isDodging = false;
    public bool IsDodging { get { return _isDodging; } }

    // Input variables
    [SerializeField] private bool _isInputActiveMovement = false;
    public bool IsInputActiveMovement { get { return _isInputActiveMovement; } }

    [SerializeField] private bool _isInputActiveRun = false;
    public bool IsInputActiveRun { get { return _isInputActiveRun; } }

    [SerializeField] private bool _isInputActiveDodge = false;
    public bool IsInputActiveDodge { get { return _isInputActiveDodge; } }
    
    [SerializeField] private Vector2 _inputMovementVector = Vector2.zero;
    public Vector2 InputMovementVector { get { return _inputMovementVector; } }

    private void Awake() 
    {
        _animator = GetComponent<Animator>();    
        _controller = GetComponent<CharacterController>();
        _inputActions = new InputActions();
        _stateManager = new PlayerStateManager(this);
        _currentState = _stateManager.GetIdleState();

        // Setting animation hashes
        _animatorHashVelocityX = Animator.StringToHash("velocityX");
        _animatorHashVelocityZ = Animator.StringToHash("velocityZ");
        _animatorHashIsRunning = Animator.StringToHash("isRunning");
        _animatorHashIsDodging = Animator.StringToHash("isDodging");
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

        // Setup dodge callbacks
        _inputActions.Player.Dodge.started += InputCallbackDodge;
        _inputActions.Player.Dodge.canceled += InputCallbackDodge;
    }

    private void InputCallbackMovement(InputAction.CallbackContext context)
    {
        _inputMovementVector = context.ReadValue<Vector2>();
        _isInputActiveMovement = _inputMovementVector != Vector2.zero;
    }

    private void InputCallbackRun(InputAction.CallbackContext context) { _isInputActiveRun = context.ReadValueAsButton(); }

    private void InputCallbackDodge(InputAction.CallbackContext context) { _isInputActiveDodge = context.ReadValueAsButton(); }

    private void Update() 
    {
        _currentState.ExecuteState();
        if (!_currentState.hasPrint) Debug.Log($"Player in state: {_currentState.GetName()}");
        _controller.Move(_currentMovementVelocity * Time.deltaTime);
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        if (_controller.isGrounded)
            _currentMovementVelocity.y = -0.05f;
        else
            _currentMovementVelocity.y = -9.81f;
    }

    private void OnDisable() { _inputActions.Disable(); }

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                  Animation Events
    private void AnimationEventDodgeStart() { _isDodging = true; }

    private void AnimationEventDodgeEnd() { _isDodging = false; }
    // -------------------------------------------------------------------------------------------------------------------------------------------------
}
