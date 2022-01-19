using System;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    public static event Action OnCurrentMovementChange;
    private const float THRESH = 1e-3f;
    
    #region Componenet References
    [SerializeField] private Player _player;
    [SerializeField] private CharacterController _controller;
    private Transform _cameraTransform;
    #endregion

    [SerializeField] private Vector3 _currentMovementVelocity;
    private Vector2 _dodgeDirection = Vector3.zero;
    private Vector3 _applyMovement = Vector3.zero;

    #region Getters
    public Vector3 CurrentMovementVelocity { get => _currentMovementVelocity; }
    #endregion

    private void OnEnable() => PlayerInputHandler.OnMovePressed += OnMovePressed;

    private void OnDisable() => PlayerInputHandler.OnMovePressed -= OnMovePressed;

    private void OnMovePressed() => _dodgeDirection = _player.InputHandler.InputMoveVector;

    private void Start() => _cameraTransform = Camera.main.transform;

    public void MoveCharacter() 
    { 
        Vector3 movementVector = GetMoveVectorTowardsCameraDirection(_currentMovementVelocity);
        ApplyGravity(movementVector);
        _controller.Move(movementVector * Time.deltaTime); 
    }

    private void ApplyGravity(Vector3 movementVector) => movementVector.y = _controller.isGrounded ? -0.05f : -9.81f;

    #region Camera rotation incorporation logic
    public void RotateTowardsCameraDirection()
    {
        if (_player.InputHandler.IsInputActiveMovement && !_player.AnimatorHandler.IsDodging)
        {
            float targetAngle = _cameraTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _player.Config.misc.cameraSensitivity * Time.deltaTime);
        }
    }

    private Vector3 GetMoveVectorTowardsCameraDirection(Vector3 movementInput)
    {
        movementInput = movementInput.x * _cameraTransform.right.normalized + movementInput.z * _cameraTransform.forward.normalized;
        movementInput.y = 0f;
        return movementInput;
    }
    #endregion

    #region Dodge logic
    public void FaceDodgeDirection()
    {
        Vector3 movementVector = new Vector3(_dodgeDirection.x, 0, _dodgeDirection.y);
        movementVector = _player.MovementHandler.GetMoveVectorTowardsCameraDirection(movementVector);
        movementVector.y = 0;

        if (movementVector != Vector3.zero)
        {
            Quaternion movementDirection = Quaternion.LookRotation(movementVector);
            _player.transform.rotation = Quaternion.Slerp(
                _player.transform.rotation,
                movementDirection,
                _player.Config.misc.cameraSensitivity * Time.deltaTime
            );
        }
    }
    #endregion
    
    #region Movement velocity update logic
    public void UpdateVelocity(MoveConfig moveConfig)
    {
        _applyMovement.x = UpdateVelocityAlongAxis(
            moveConfig, 
            _player.InputHandler.InputMoveVector.x, 
            _currentMovementVelocity.x
        );
        _applyMovement.z = UpdateVelocityAlongAxis(
            moveConfig, 
            _player.InputHandler.InputMoveVector.y, 
            _currentMovementVelocity.z
        );

        UpdateCurrentVelocity();
    }

    private float UpdateVelocityAlongAxis(MoveConfig moveConfig, float inputVector, float currentVelocity)
    {
        float velocity = currentVelocity;

        if (IsVelocityNonZero(inputVector))
            velocity = AccelarateAlongAxis(moveConfig, inputVector, currentVelocity);
        else
            velocity = DecelarateAlongAxis(moveConfig, currentVelocity);

        velocity = Mathf.Clamp(velocity, -moveConfig.maxVelocity, moveConfig.maxVelocity);
        return velocity;
    }

    private float AccelarateAlongAxis(MoveConfig moveConfig, float inputVelocity, float currentVelocity)
    {
        // Player pressed button to move in the positive direction of axis
        if (inputVelocity > THRESH)
            return currentVelocity + MakeFrameIndependent(moveConfig.accelaration);

        // Player pressed button to move in the negative direction of axis
        else if (inputVelocity < -THRESH)
            return currentVelocity - MakeFrameIndependent(moveConfig.accelaration);
        
        return 0f;
    }

    private float DecelarateAlongAxis(MoveConfig moveConfig, float currentVelocity)
    {
        // Player hasn't pressed any button for this axis but was moving in the positive direction.
        if (currentVelocity > THRESH)
            return currentVelocity - MakeFrameIndependent(moveConfig.decelaration);
        
        // Player hasn't pressed any button for this axis but was moving in the negative direction.
        else if (currentVelocity < -THRESH)
            return currentVelocity + MakeFrameIndependent(moveConfig.decelaration);
        
        return 0f;
    }

    public void Decelarate()
    {
        if (IsVelocityNonZero(_currentMovementVelocity.x))
            _applyMovement.x = Mathf.Clamp(
                DecelarateAlongAxis(_player.Config.run, _currentMovementVelocity.x), 
                -_player.Config.run.maxVelocity, 
                _player.Config.run.maxVelocity
            );
        else
            _applyMovement.x = 0f;

        if (IsVelocityNonZero(_currentMovementVelocity.z))
            _applyMovement.z= Mathf.Clamp(
                DecelarateAlongAxis(_player.Config.run, _currentMovementVelocity.z), 
                -_player.Config.run.maxVelocity, 
                _player.Config.run.maxVelocity
            );
        else
            _applyMovement.z = 0f;

        UpdateCurrentVelocity();
    }

    private static bool IsVelocityNonZero(float velocity) => velocity < -THRESH || velocity > THRESH;

    private float MakeFrameIndependent(float value) => value * Time.deltaTime;

    private void UpdateCurrentVelocity()
    {
        _currentMovementVelocity = _applyMovement;
        OnCurrentMovementChange?.Invoke();
    }
    #endregion
}
