using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Player))]
public class PlayerAnimationHandler : MonoBehaviour
{
    // =================================================================================================================================================
    //                                                                  Constants
    // =================================================================================================================================================
    private const string _ANIMATION_PARAMETER_VELOCITY_X = "velocityX";
    private const string _ANIMATION_PARAMETER_VELOCITY_Z = "velocityZ";
    private const string _ANIMATION_PARAMETER_IS_RUNNING = "isRunning";
    private const string _ANIMATION_PARAMETER_IS_DODGING = "isDodging";
    private const string _ANIMATION_PARAMETER_IS_MELEE_ATTACKING = "isMeleeAttacking";
    private const string _ANIMATION_PARAMETER_MELEE_ATK_NO = "meleeAttackNumber";

    // =================================================================================================================================================
    //                                                          References and Variables
    // =================================================================================================================================================

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    // Componenet References
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    private Animator _animator;
    private Player _player;

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    // Animation Hash Variables
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    private int _animatorHashVelocityX;
    private int _animatorHashVelocityZ;
    private int _animatorHashIsRunning;
    private int _animatorHashIsDodging;
    private int _animatorHashIsMeleeAttacking;
    private int _animatorHashMeleeAttackNumber;

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    // Variables
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    private bool _isDodging = false;
    [SerializeField] private int _meleeAttackNumber = 0;
    [SerializeField] private bool _isMeleeAttacking = false;

    // =================================================================================================================================================
    //                                                              Getters and Setters
    // =================================================================================================================================================
    public Animator Animator { get { return _animator; } }
    public bool IsDodging { get { return _isDodging; } }
    public bool IsMeleeAttacking { get { return _isMeleeAttacking; } }
    public int MeleeAttackNumber { get { return _meleeAttackNumber; } }


    // =================================================================================================================================================
    //                                                                  Functions
    // =================================================================================================================================================
    private void Awake() 
    {
        _animator = GetComponent<Animator>();    
        _player = GetComponent<Player>();
        _animatorHashVelocityX = Animator.StringToHash(_ANIMATION_PARAMETER_VELOCITY_X);
        _animatorHashVelocityZ = Animator.StringToHash(_ANIMATION_PARAMETER_VELOCITY_Z);
        _animatorHashIsRunning = Animator.StringToHash(_ANIMATION_PARAMETER_IS_RUNNING);
        _animatorHashIsDodging = Animator.StringToHash(_ANIMATION_PARAMETER_IS_DODGING);
        _animatorHashIsMeleeAttacking = Animator.StringToHash(_ANIMATION_PARAMETER_IS_MELEE_ATTACKING);
        _animatorHashMeleeAttackNumber = Animator.StringToHash(_ANIMATION_PARAMETER_MELEE_ATK_NO);
    }

    public void SetAnimationValueVelocityX(float value) { _animator.SetFloat(_animatorHashVelocityX, value); }

    public void SetAnimationValueVelocityZ(float value) { _animator.SetFloat(_animatorHashVelocityZ, value); }

    public void SetAnimationValueIsRunning(bool value) { _animator.SetBool(_animatorHashIsRunning, value); }

    public void SetAnimationValueIsDodging(bool value) { _animator.SetBool(_animatorHashIsDodging, value); }

    public void SetAnimationValueIsMeleeAttacking(bool value) { _animator.SetBool(_animatorHashIsMeleeAttacking, value); }

    public void IncrementMeleeAttackNumber()
    {
        _meleeAttackNumber++;
        _animator.SetInteger(_animatorHashMeleeAttackNumber, _meleeAttackNumber);
    }

    public void ResetMeleeAttackNumber() 
    { 
        _meleeAttackNumber = 0; 
        _animator.SetInteger(_animatorHashMeleeAttackNumber, _meleeAttackNumber);
    }


    // =================================================================================================================================================
    //                                                              Animation Event Functions
    // =================================================================================================================================================
    private void AnimationEventDodgeStart() { _isDodging = true; }

    private void AnimationEventDodgeEnd() { _isDodging = false; }

    private void AnimationEventMeleeAttackStart() { _isMeleeAttacking = true; }

    private void AnimationEventMeleeAttackEnd() { _isMeleeAttacking = false; }
    // -------------------------------------------------------------------------------------------------------------------------------------------------
}
