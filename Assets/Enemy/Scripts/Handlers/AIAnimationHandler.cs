using System.Collections;
using UnityEngine;

public class AIAnimationHandler : MonoBehaviour
{
    [SerializeField] private AIAgent _aiAgent;
    [SerializeField] private Animator _animator;

    private int _animationHashMovementSpeed;

    private void Awake()
    {
        _animationHashMovementSpeed = Animator.StringToHash("MovementSpeed");
    }

    public void SetAnimationValueMovementSpeed(float value) { _animator.SetFloat(_animationHashMovementSpeed, value); }
}
