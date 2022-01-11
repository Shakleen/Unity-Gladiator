using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : MonoBehaviour
{
    // =================================================================================================================================================
    //                                                          References and Variables
    // =================================================================================================================================================
    
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    // Constants
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    private const string _ACTION_NAME_MOVE = "Move";
    private const string _ACTION_NAME_RUN = "Run";
    private const string _ACTION_NAME_DODGE = "Dodge";
    private const string _ACTION_NAME_LOOK = "Look";
    
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    // Componenet References
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    private PlayerInput _playerInput;
    private InputAction _inputActionMove;
    private InputAction _inputActionRun;
    private InputAction _inputActionDodge;
    private InputAction _inputActionLook;
    
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    // Input variables
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [SerializeField] private Vector2 _inputMoveVector = Vector2.zero;
    [SerializeField] private Vector2 _inputLookVector = Vector2.zero;
    [SerializeField] private bool _isInputActiveMovement = false;
    [SerializeField] private bool _isInputActiveRun = false;
    [SerializeField] private bool _isInputActiveDodge = false;
    // -------------------------------------------------------------------------------------------------------------------------------------------------


    // =================================================================================================================================================
    //                                                                      Getters
    // =================================================================================================================================================
    public Vector2 InputMoveVector { get { return _inputMoveVector; } }
    public Vector2 InputLookVector { get { return _inputLookVector; } }
    public bool IsInputActiveMovement { get { return _isInputActiveMovement; } }
    public bool IsInputActiveRun { get { return _isInputActiveRun; } }
    public bool IsInputActiveDodge { get { return _isInputActiveDodge; } }
    // -------------------------------------------------------------------------------------------------------------------------------------------------


    // =================================================================================================================================================
    //                                                                      Functions
    // =================================================================================================================================================

    private void Awake() 
    {
        _playerInput = GetComponent<PlayerInput>(); 
        _inputActionMove = _playerInput.actions[_ACTION_NAME_MOVE];
        _inputActionRun = _playerInput.actions[_ACTION_NAME_RUN];
        _inputActionDodge = _playerInput.actions[_ACTION_NAME_DODGE];
        _inputActionLook = _playerInput.actions[_ACTION_NAME_LOOK];
    }

    private void OnEnable() 
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
    }

    private void InputCallbackMovement(InputAction.CallbackContext context)
    {
        _inputMoveVector = context.ReadValue<Vector2>();
        _isInputActiveMovement = _inputMoveVector != Vector2.zero;
    }

    private void InputCallbackLook(InputAction.CallbackContext context) { _inputLookVector = context.ReadValue<Vector2>(); }

    private void InputCallbackRun(InputAction.CallbackContext context) { _isInputActiveRun = context.ReadValueAsButton(); }

    private void InputCallbackDodge(InputAction.CallbackContext context) { _isInputActiveDodge = context.ReadValueAsButton(); }
}
