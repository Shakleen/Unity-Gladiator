using System.Collections;
using UnityEngine;

public class AIAnimationHandler : MonoBehaviour
{
    private const string ANIMATION_PARAM_MOVEMENT_SPEED = "MovementSpeed";
    private const string ANIMATION_PARAM_IS_DEAD = "IsDead";

    [SerializeField] private AIAgent _aiAgent;
    [SerializeField] private Animator _animator;

    private int _animationHashMovementSpeed;
    private int _animationHashIsDead;

    private void Awake()
    {
        _animationHashMovementSpeed = Animator.StringToHash(ANIMATION_PARAM_MOVEMENT_SPEED);
        _animationHashIsDead = Animator.StringToHash(ANIMATION_PARAM_IS_DEAD);
    }

    public void SetAnimationValueMovementSpeed(float value) { _animator.SetFloat(_animationHashMovementSpeed, value); }

    public void SetAnimationValueIsDead(bool value) { _animator.SetBool(_animationHashIsDead, value); }

    public bool IsAnimationPlaying() {return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f;}
}
