using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Player))]
public class PlayerMovementController : MonoBehaviour
{
    // =================================================================================================================================================
    //                                                          Editor tunable variables
    // =================================================================================================================================================
    [Tooltip("Show debug log texts")]
    [SerializeField] private bool _showDebugText = false;

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [Header("Walk Variables")]
    [Tooltip("Maximum forward veloctiy the player can walk")] [SerializeField] [Range(0.5f, 5.0f)] private float _maxForwardWalkVelocity = 1.0f;
    [Tooltip("Accelaration used to go towards maximum walk velocity")] [SerializeField] [Range(0.5f, 5.0f)] private float _accelarationWalk = 2.0f;
    [Tooltip("Deceleration used to go towards idle or zero velocity")] [SerializeField] [Range(0.5f, 5.0f)] private float _decelarationWalk = 2.0f;
    // -------------------------------------------------------------------------------------------------------------------------------------------------

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [Header("Run Variables")]
    [Tooltip("Maximum veloctiy the player can run")] [SerializeField] [Range(0.5f, 5.0f)] private float _maxVelocityRun = 2.0f;
    [Tooltip("Accelaration used to go towards maximum run velocity")] [SerializeField] [Range(0.5f, 5.0f)] private float _accelarationRun = 2.0f;
    [Tooltip("Deceleration used to go towards max walk velocity")] [SerializeField] [Range(0.5f, 5.0f)] private float _decelarationRun = 1.0f;
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [Header("Camera variables")]
    [Tooltip("Camera movement sensitivity")] [SerializeField] [Range(1f, 10.0f)] private float _cameraSensitivity = 5.0f;
    // -------------------------------------------------------------------------------------------------------------------------------------------------


    // =================================================================================================================================================
    //                                                          References and Variables
    // =================================================================================================================================================
    
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    // Componenet References
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    private CharacterController _controller;
    private PlayerStateManager _stateManager;
    private Transform _cameraTranform;
    private Player _player;

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    // State veriables
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    private PlayerBaseState _currentState;
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    // Movement variables
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [SerializeField] private Vector3 _currentMovementVelocity = Vector3.zero;
    public Vector3 CurrentMovementVelocity { get { return _currentMovementVelocity; } set { _currentMovementVelocity = value; } }
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
    // -------------------------------------------------------------------------------------------------------------------------------------------------


    // =================================================================================================================================================
    //                                                              Getters and Setters
    // =================================================================================================================================================
    public float MaxForwardWalkVelocity { get { return _maxForwardWalkVelocity; } }
    public float AccelarationWalk { get { return _accelarationWalk; } }
    public float DecelarationWalk { get { return _decelarationWalk; } }
    public float MaxVelocityRun { get { return _maxVelocityRun; } }
    public float AccelarationRun { get { return _accelarationRun; } }
    public float DecelarationRun { get { return _decelarationRun; } }


    // =================================================================================================================================================
    //                                                                  Functions
    // =================================================================================================================================================

    private void Awake() 
    {    
        _controller = GetComponent<CharacterController>();
        _player = GetComponent<Player>();
        _stateManager = new PlayerStateManager(_player);
        _currentState = _stateManager.GetIdleState();
    }

    private void Start() { _cameraTranform = Camera.main.transform; }

    private void Update()
    {
        _currentState.ExecuteState();
        if (!_currentState.hasPrint) Debug.Log($"Player in state: {_currentState.GetName()}");
        RotateTowardsCameraDirection();
        _controller.Move(GetMoveVectorTowardsCameraDirection() * Time.deltaTime);
        ApplyGravity();
    }

    private void RotateTowardsCameraDirection()
    {
        if (_player.InputHandler.IsInputActiveMovement)
        {
            float targetAngle = _cameraTranform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _cameraSensitivity * Time.deltaTime);
        }
    }

    private Vector3 GetMoveVectorTowardsCameraDirection()
    {
        Vector3 appliedMove = _currentMovementVelocity;
        appliedMove = appliedMove.x * _cameraTranform.right.normalized + appliedMove.z * _cameraTranform.forward.normalized;
        appliedMove.y = 0f;
        return appliedMove;
    }

    private void ApplyGravity()
    {
        if (_controller.isGrounded)
            _currentMovementVelocity.y = -0.05f;
        else
            _currentMovementVelocity.y = -9.81f;
    }
}
