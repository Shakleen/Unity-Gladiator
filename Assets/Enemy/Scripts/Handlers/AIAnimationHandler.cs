using UnityEngine;

public class AIAnimationHandler : MonoBehaviour
{   
    #region Component references
    [SerializeField] private AIAgent _aiAgent;
    [SerializeField] private Animator _animator;
    #endregion

    #region Animation parameter hashes
    private int _animationHashMovementSpeed;
    private int _animationHashIsDead;
    private int _animationHashIsTaunting;
    private int _animationHashIsAttacking;
    #endregion

    private bool _isTaunting, _isAttacking;

    public bool IsTaunting { get => _isTaunting; }
    public bool IsAttacking { get => _isAttacking; }

    private void Awake()
    {
        _animationHashMovementSpeed = Animator.StringToHash("movementSpeed");
        _animationHashIsTaunting = Animator.StringToHash("isTaunting");
        _animationHashIsAttacking = Animator.StringToHash("isAttacking");
        _animationHashIsDead = Animator.StringToHash("IsDead");
    }

    public void SetAnimationValueMovementSpeed(float value) => _animator.SetFloat(_animationHashMovementSpeed, value);
    public void SetAnimationValueIsDead(bool value) => _animator.SetBool(_animationHashIsDead, value);
    public void SetAnimationValueIsTaunting(bool value) => _animator.SetBool(_animationHashIsTaunting, value);
    public void SetAnimationValueIsAttacking(bool value) => _animator.SetBool(_animationHashIsAttacking, value);
    public void SetAnimatorActiveStatus(bool status) => _animator.enabled = status;

    public bool IsAnimationPlaying() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f;

    #region Animation event callbacks
    private void AnimationEventTauntStart() => _isTaunting = true;
    private void AnimationEventTauntEnd() => _isTaunting = false;
    private void AnimationEventMeleeAttackStart() => _isAttacking = true;
    private void AnimationEventMeleeAttackEnd() => _isAttacking = false;
    #endregion
}
