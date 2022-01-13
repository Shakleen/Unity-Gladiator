using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private const float MOVEMENT_TRHESH = 1e-3f;

    // =================================================================================================================================================
    //                                                          Editor tunable variables
    // =================================================================================================================================================
    [SerializeField] private Player _player;
    [SerializeField] private CharacterController _controller;

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [Header("Walk Variables")]
    [Tooltip("Maximum forward veloctiy the player can walk")] [SerializeField] [Range(0.5f, 10.0f)] private float _maxForwardWalkVelocity = 1.0f;
    [Tooltip("Accelaration used to go towards maximum walk velocity")] [SerializeField] [Range(0.5f, 10.0f)] private float _accelarationWalk = 2.0f;
    [Tooltip("Deceleration used to go towards idle or zero velocity")] [SerializeField] [Range(0.5f, 10.0f)] private float _decelarationWalk = 2.0f;
    // -------------------------------------------------------------------------------------------------------------------------------------------------

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [Header("Run Variables")]
    [Tooltip("Maximum veloctiy the player can run")] [SerializeField] [Range(0.5f, 20.0f)] private float _maxVelocityRun = 2.0f;
    [Tooltip("Accelaration used to go towards maximum run velocity")] [SerializeField] [Range(0.5f, 20.0f)] private float _accelarationRun = 2.0f;
    [Tooltip("Deceleration used to go towards max walk velocity")] [SerializeField] [Range(0.5f, 20.0f)] private float _decelarationRun = 1.0f;
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [Header("Camera variables")]
    [Tooltip("Camera movement sensitivity")] [SerializeField] [Range(1f, 10.0f)] private float _cameraSensitivity = 5.0f;
    // -------------------------------------------------------------------------------------------------------------------------------------------------

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [Header("Player attack variables")]
    [Tooltip("Melee attack combo time limit")] [SerializeField] [Range(1f, 10.0f)] private float _meleeAttackComboTimeLimit = 2.0f;
    // -------------------------------------------------------------------------------------------------------------------------------------------------

    
    // =================================================================================================================================================
    //                                                          References and Variables
    // =================================================================================================================================================
    
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    // Componenet References
    // -------------------------------------------------------------------------------------------------------------------------------------------------
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
    public float MaxForwardWalkVelocity { get { return _maxForwardWalkVelocity; } }
    public float AccelarationWalk { get { return _accelarationWalk; } }
    public float DecelarationWalk { get { return _decelarationWalk; } }
    public float MaxVelocityRun { get { return _maxVelocityRun; } }
    public float AccelarationRun { get { return _accelarationRun; } }
    public float DecelarationRun { get { return _decelarationRun; } }
    public float MeleeAttackComboTimeLimit { get { return _meleeAttackComboTimeLimit; } }
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
    public float CameraSensitivity { get { return _cameraSensitivity; } }


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
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _cameraSensitivity * Time.deltaTime);
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