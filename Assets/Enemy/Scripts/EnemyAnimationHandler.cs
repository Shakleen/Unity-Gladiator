using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour 
{   
    #region Component references
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Animator _animator;
    #endregion

    #region Animation parameter hashes
    private int _hashIsDead;
    private int _hashMovementSpeed;
    private int _hashIsTaunting;
    private int _hashIsAttacking;
    #endregion

    public bool IsTaunting { get; private set; }
    public bool IsAttacking { get; private set; }

    private void Awake()
    {
        _hashIsDead = Animator.StringToHash("isDead");
        _hashMovementSpeed = Animator.StringToHash("movementSpeed");
        _hashIsTaunting = Animator.StringToHash("isTaunting");
        _hashIsAttacking = Animator.StringToHash("isAttacking");
    }

    public void SetParameterIsDead(bool value) => _animator.SetBool(_hashIsDead, value);

    public void SetParameterMovementSpeed(float value) => _animator.SetFloat(_hashMovementSpeed, value);

    public void SetParameterIsTaunting(bool value) 
    {
        _animator.SetBool(_hashIsTaunting, value);
        IsTaunting = value;
    }

    public void SetParameterIsAttacking(bool value) 
    {
        _animator.SetBool(_hashIsAttacking, value);
        IsAttacking = value;
    }
    
    public void SetAnimatorActiveStatus(bool status) => _animator.enabled = status;

    public bool IsAnimationPlaying() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f;

    public void AnimationEventFootDown() => _enemy.audioSource.PlayOneShot(_enemy.Config.Sound.GetRandomFootStepSound());

    public void AnimationEventTauntEnd() => SetParameterIsTaunting(false);
}
