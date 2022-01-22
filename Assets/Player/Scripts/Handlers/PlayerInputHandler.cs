using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    #region Action events
    public static event Action OnAttackPressed;
    public static event Action OnDodgePressed;
    public static event Action OnRunPressed;
    public static event Action OnMovePressed;
    public static event Action<bool> OnPausePressed;
    #endregion
    
    [SerializeField] private PlayerInput _playerInput;
    
    #region Input action variables
    private InputAction _inputActionMove;
    private InputAction _inputActionRun;
    private InputAction _inputActionDodge;
    private InputAction _inputActionLook;
    private InputAction _inputActionAttack;
    private InputAction _inputActionPause;
    #endregion
    
    #region Input storing variables
    [SerializeField] private Vector2 _inputMoveVector = Vector2.zero;
    [SerializeField] private Vector2 _inputLookVector = Vector2.zero;
    [SerializeField] private bool _isInputActiveMovement = false;
    [SerializeField] private bool _isInputActiveRun = false;
    [SerializeField] private bool _isInputActiveDodge = false;
    [SerializeField] private bool _isInputActiveAttack = false;
    #endregion

    #region Getters
    public Vector2 InputMoveVector { get => _inputMoveVector; }
    public Vector2 InputLookVector { get => _inputLookVector; }
    public bool IsInputActiveMovement { get => _isInputActiveMovement; }
    public bool IsInputActiveRun { get => _isInputActiveRun; }
    public bool IsInputActiveDodge { get => _isInputActiveDodge; }
    public bool IsInputActiveMeleeAttack { get => _isInputActiveAttack; }
    #endregion

    private bool _isPaused = false;

    private void Awake() 
    {
        _inputActionMove = _playerInput.actions["Move"];
        _inputActionRun = _playerInput.actions["Run"];
        _inputActionDodge = _playerInput.actions["Dodge"];
        _inputActionLook = _playerInput.actions["Look"];
        _inputActionAttack = _playerInput.actions["Attack"];
        _inputActionPause = _playerInput.actions["Pause"];
    }

    private void OnEnable()
    {
        EnableInputActions();
        SetupInputCallbacks();
        PlayerStatusHandler.OnDeath += DisableInputActions;
    }

    private void OnDisable() 
    {
        DisableInputActions();
        PlayerStatusHandler.OnDeath -= DisableInputActions;
    }

    #region Input callbacks
    private void SetupInputCallbacks()
    {
        // Setup movement callbacks
        _inputActionMove.started += InputCallbackMovement;
        _inputActionMove.canceled += InputCallbackMovement;
        _inputActionMove.performed += InputCallbackMovement;

        // Setup movement callbacks
        _inputActionLook.performed += InputCallbackLook;

        // Setup run callbacks
        _inputActionRun.started += InputCallbackRun;
        _inputActionRun.canceled += InputCallbackRun;

        // Setup dodge callbacks
        _inputActionDodge.started += InputCallbackDodge;
        _inputActionDodge.canceled += InputCallbackDodge;

        // Setup attack callbacks
        _inputActionAttack.started += InputCallbackAttack;
        _inputActionAttack.canceled += InputCallbackAttack;

        // Setup pause callback
        _inputActionPause.started += InputCallbackPause;
    }

    private void InputCallbackMovement(InputAction.CallbackContext context)
    {
        if (!_isPaused)
        {
            _inputMoveVector = context.ReadValue<Vector2>();
            _isInputActiveMovement = _inputMoveVector != Vector2.zero;
            OnMovePressed?.Invoke();
        }
    }

    private void InputCallbackLook(InputAction.CallbackContext context) 
    {  
        if (!_isPaused)
            _inputLookVector = context.ReadValue<Vector2>();
    }

    private void InputCallbackRun(InputAction.CallbackContext context) 
    { 
        if (!_isPaused)
        {
            _isInputActiveRun = context.ReadValueAsButton(); 
            OnRunPressed?.Invoke();
        }
    }

    private void InputCallbackDodge(InputAction.CallbackContext context) 
    { 
        if (!_isPaused)
        {
            _isInputActiveDodge = context.ReadValueAsButton(); 
            OnDodgePressed?.Invoke();
        }
    }

    private void InputCallbackAttack(InputAction.CallbackContext context) 
    { 
        if (!_isPaused)
        {
            _isInputActiveAttack = context.ReadValueAsButton(); 
            if (_isInputActiveAttack) OnAttackPressed?.Invoke();
        }
    }

    private void InputCallbackPause(InputAction.CallbackContext context) 
    {
        _isPaused = !_isPaused;
        OnPausePressed?.Invoke(_isPaused);
    }
    #endregion

    private void EnableInputActions()
    {
        _inputActionLook.Enable();
        _inputActionMove.Enable();
        _inputActionRun.Enable();
        _inputActionDodge.Enable();
        _inputActionAttack.Enable();
    }

    private void DisableInputActions()
    {
        _inputActionLook.Disable();
        _inputActionMove.Disable();
        _inputActionRun.Disable();
        _inputActionDodge.Disable();
        _inputActionAttack.Disable();
    }
}
