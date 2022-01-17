using System;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    public static event Action OnCurrentMovementChange;
    private const float MOVEMENT_TRHESH = 1e-3f;
    
    #region Componenet References
    [SerializeField] private Player _player;
    [SerializeField] private CharacterController _controller;
    private Transform _cameraTransform;
    #endregion

    private Vector3 _currentMovementVelocity = Vector3.zero;
    private Vector2 _dodgeDirection = Vector3.zero;

    #region Getters and Setters
    public float THRESH { get { return MOVEMENT_TRHESH; } }
    public Vector3 CurrentMovementVelocity 
    {
        get => _currentMovementVelocity;
        set
        {
            _currentMovementVelocity = value;
            OnCurrentMovementChange?.Invoke();
        }
    }
    #endregion

    private void OnEnable() => PlayerInputHandler.OnMovePressed += OnMovePressed;

    private void OnDisable() => PlayerInputHandler.OnMovePressed -= OnMovePressed;

    private void OnMovePressed() => _dodgeDirection = _player.InputHandler.InputMoveVector;

    private void Start() => _cameraTransform = Camera.main.transform;

    private Vector3 GetMoveVectorTowardsCameraDirection(Vector3 movementInput)
    {
        movementInput = movementInput.x * _cameraTransform.right.normalized + movementInput.z * _cameraTransform.forward.normalized;
        movementInput.y = 0f;
        return movementInput;
    }

    #region Walk and Run logic
    public void MoveCharacter() 
    { 
        Vector3 movementVector = GetMoveVectorTowardsCameraDirection(_currentMovementVelocity);
        ApplyGravity(movementVector);
        _controller.Move(movementVector * Time.deltaTime); 
    }

    private void ApplyGravity(Vector3 movementVector) => movementVector.y = _controller.isGrounded ? -0.05f : -9.81f;

    public void RotateTowardsCameraDirection()
    {
        if (_player.InputHandler.IsInputActiveMovement && !_player.AnimatorHandler.IsDodging)
        {
            float targetAngle = _cameraTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _player.Config.misc.cameraSensitivity * Time.deltaTime);
        }
    }

    public void StopMovement() => _currentMovementVelocity = new Vector3(0, _currentMovementVelocity.y, 0);
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
}
