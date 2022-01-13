using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    // =================================================================================================================================================
    //                                                          References and Variables
    // =================================================================================================================================================
    
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    // Constants
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    private const float MOVEMENT_TRHESH = 1e-3f;
    
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    // Componenet References
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [SerializeField] private Player _player;
    [SerializeField] private CharacterController _controller;
    private PlayerStateManager _stateManager;
    private Transform _cameraTransform;

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    // State veriables
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    private PlayerBaseState _currentState;

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    // Movement variables
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [SerializeField] private Vector3 _currentMovementVelocity = Vector3.zero;
    // -------------------------------------------------------------------------------------------------------------------------------------------------


    // =================================================================================================================================================
    //                                                              Getters and Setters
    // =================================================================================================================================================
    public float THRESH { get { return MOVEMENT_TRHESH; } }
    public Transform CameraTransform { get { return _cameraTransform; } }
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public float CurrentMovementVelocityX 
    { 
        get { return _currentMovementVelocity.x; } 
        set 
        { 
            _currentMovementVelocity.x = value; 
            _player.AnimatorHandler.SetAnimationValueVelocityX(value);
        } 
    }
    public float CurrentMovementVelocityZ 
    { 
        get { return _currentMovementVelocity.z; } 
        set 
        { 
            _currentMovementVelocity.z = value; 
            _player.AnimatorHandler.SetAnimationValueVelocityZ(value);
        } 
    }


    // =================================================================================================================================================
    //                                                                  Functions
    // =================================================================================================================================================

    private void Awake() 
    {    
        _stateManager = new PlayerStateManager(_player);
        _currentState = _stateManager.GetIdleState();
    }

    private void Start() { _cameraTransform = Camera.main.transform; }

    private void Update()
    {
        _currentState.ExecuteState();
        ApplyGravity();
        if (!_currentState.hasPrint) Debug.Log($"Player in state: {_currentState.GetName()}");
    }

    public void MoveCharacter() 
    { 
        Vector3 movementVector = GetMoveVectorTowardsCameraDirection(_currentMovementVelocity);
        _controller.Move(movementVector * Time.deltaTime); 
    }

    public void RotateTowardsCameraDirection()
    {
        if (_player.InputHandler.IsInputActiveMovement && !_player.AnimatorHandler.IsDodging)
        {
            float targetAngle = _cameraTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _player.Config.CameraSensitivity * Time.deltaTime);
        }
    }

    public Vector3 GetMoveVectorTowardsCameraDirection(Vector3 movementInput)
    {
        movementInput = movementInput.x * _cameraTransform.right.normalized + movementInput.z * _cameraTransform.forward.normalized;
        movementInput.y = 0f;
        return movementInput;
    }

    private void ApplyGravity()
    {
        if (_controller.isGrounded)
            _currentMovementVelocity.y = -0.05f;
        else
            _currentMovementVelocity.y = -9.81f;
    }

    public void StopMovement() 
    { 
        CurrentMovementVelocityX = 0; 
        CurrentMovementVelocityZ = 0; 
    }
}
