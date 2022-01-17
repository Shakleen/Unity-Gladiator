using System.Collections;
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
    #endregion

    private void Awake()
    {
        _animationHashMovementSpeed = Animator.StringToHash("MovementSpeed");
        _animationHashIsDead = Animator.StringToHash("IsDead");
    }

    public void SetAnimationValueMovementSpeed(float value) => _animator.SetFloat(_animationHashMovementSpeed, value);

    public void SetAnimationValueIsDead(bool value) => _animator.SetBool(_animationHashIsDead, value);

    public void SetAnimatorActiveStatus(bool status) => _animator.enabled = status;

    public bool IsAnimationPlaying() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f;
}
