using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour 
{   
    #region Component references
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Animator _animator;
    #endregion

    #region Animation parameter hashes
    private int _animationHashIsDead;
    #endregion

    private void Awake()
    {
        _animationHashIsDead = Animator.StringToHash("isDead");
    }

    public void SetAnimationValueIsDead(bool value) => _animator.SetBool(_animationHashIsDead, value);
    
    public void SetAnimatorActiveStatus(bool status) => _animator.enabled = status;

    public bool IsAnimationPlaying() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f;
}
