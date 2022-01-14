using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
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
    private Transform _cameraTransform;

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    // Movement variables
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [SerializeField] private Vector3 _currentMovementVelocity = Vector3.zero;
    // -------------------------------------------------------------------------------------------------------------------------------------------------


    // =================================================================================================================================================
    //                                                              Getters and Setters
    // =================================================================================================================================================
    public float THRESH { get { return MOVEMENT_TRHESH; } }
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

    private void Start() { _cameraTransform = Camera.main.transform; }

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

    public void ApplyGravity()
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
