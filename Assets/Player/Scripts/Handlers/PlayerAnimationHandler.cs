using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Player))]
public class PlayerAnimationHandler : MonoBehaviour
{
    // =================================================================================================================================================
    //                                                          Editor tunable variables
    // =================================================================================================================================================
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [Header("Walk Variables")]
    [Tooltip("Walk animation speed multiplier")] [SerializeField] [Range(0.5f, 5.0f)] private float _moveSpeedMultiplier = 1.0f;
    [Tooltip("Idle Melee animation speed multiplier")] [SerializeField] [Range(0.5f, 5.0f)] private float _idleMeleeAttackSpeedMultiplier = 1.0f;
    [Tooltip("Walk Melee animation speed multiplier")] [SerializeField] [Range(0.5f, 5.0f)] private float _walkMeleeAttackSpeedMultiplier = 1.0f;
    [Tooltip("Run Melee animation speed multiplier")] [SerializeField] [Range(0.5f, 5.0f)] private float _runMeleeAttackSpeedMultiplier = 1.0f;
    [Tooltip("Normal dodge animation speed multiplier")] [SerializeField] [Range(0.5f, 5.0f)] private float _normalDodgeSpeedMultiplier = 1.0f;
    [Tooltip("Run dodge animation speed multiplier")] [SerializeField] [Range(0.5f, 5.0f)] private float _runDodgeSpeedMultiplier = 1.0f;

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    // =================================================================================================================================================
    //                                                                  Constants
    // =================================================================================================================================================
    private const string _ANIMATION_PARAMETER_VELOCITY_X = "velocityX";
    private const string _ANIMATION_PARAMETER_VELOCITY_Z = "velocityZ";
    private const string _ANIMATION_PARAMETER_IS_MOVING = "isMoving";
    private const string _ANIMATION_PARAMETER_IS_RUNNING = "isRunning";
    private const string _ANIMATION_PARAMETER_IS_DODGING = "isDodging";
    private const string _ANIMATION_PARAMETER_IS_MELEE_ATTACKING = "isMeleeAttacking";
    private const string _ANIMATION_PARAMETER_MELEE_ATK_NO = "meleeAttackNumber";
    private const string _ANIMATION_PARAMETER_MULTIPLIER_MOVE = "moveSpeedMultiplier";
    private const string _ANIMATION_PARAMETER_MULTIPLIER_MELEE_IDLE = "idleMeleeAttackSpeedMultiplier";
    private const string _ANIMATION_PARAMETER_MULTIPLIER_MELEE_WALK = "walkMeleeAttackSpeedMultiplier";
    private const string _ANIMATION_PARAMETER_MULTIPLIER_MELEE_RUN = "runMeleeAttackSpeedMultiplier";
    private const string _ANIMATION_PARAMETER_MULTIPLIER_DODGE_NORMAL = "normalDodgeSpeedMultiplier";
    private const string _ANIMATION_PARAMETER_MULTIPLIER_DODGE_RUN = "runDodgeSpeedMultiplier";

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
    private int _animatorHashVelocityX, _animatorHashVelocityZ;
    private int _animatorHashIsMoving, _animatorHashIsRunning, _animatorHashIsDodging, _animatorHashIsMeleeAttacking;
    private int _animatorHashMeleeAttackNumber;
    private int _animatorHashMultiplierMove, _animatorHashMultiplierDodgeNormal, _animatorHashMultiplierDodgeRun;
    private int _animatorHashMultiplierIdleMelee, _animatorHashMultiplierWalkMelee, _animatorHashMultiplierRunMelee;

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    // Variables
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    private bool _isDodging = false;
    [SerializeField] private int _meleeAttackNumber = 0;
    [SerializeField] private bool _isMeleeAttacking = false;

    // =================================================================================================================================================
    //                                                              Getters and Setters
    // =================================================================================================================================================
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
        _animatorHashIsMoving = Animator.StringToHash(_ANIMATION_PARAMETER_IS_MOVING);
        _animatorHashIsRunning = Animator.StringToHash(_ANIMATION_PARAMETER_IS_RUNNING);
        _animatorHashIsDodging = Animator.StringToHash(_ANIMATION_PARAMETER_IS_DODGING);
        _animatorHashIsMeleeAttacking = Animator.StringToHash(_ANIMATION_PARAMETER_IS_MELEE_ATTACKING);
        _animatorHashMeleeAttackNumber = Animator.StringToHash(_ANIMATION_PARAMETER_MELEE_ATK_NO);
        _animatorHashMultiplierMove = Animator.StringToHash(_ANIMATION_PARAMETER_MULTIPLIER_MOVE);
        _animatorHashMultiplierIdleMelee = Animator.StringToHash(_ANIMATION_PARAMETER_MULTIPLIER_MELEE_IDLE);
        _animatorHashMultiplierWalkMelee = Animator.StringToHash(_ANIMATION_PARAMETER_MULTIPLIER_MELEE_WALK);
        _animatorHashMultiplierRunMelee = Animator.StringToHash(_ANIMATION_PARAMETER_MULTIPLIER_MELEE_RUN);
        _animatorHashMultiplierDodgeNormal = Animator.StringToHash(_ANIMATION_PARAMETER_MULTIPLIER_DODGE_NORMAL);
        _animatorHashMultiplierDodgeRun = Animator.StringToHash(_ANIMATION_PARAMETER_MULTIPLIER_DODGE_RUN);
    }

    private void OnEnable()
    {
        SetMoveAnimationSpeedMultiplier(_moveSpeedMultiplier);
        SetIdleMeleeAnimationSpeedMultiplier(_idleMeleeAttackSpeedMultiplier);
        SetWalkMeleeAnimationSpeedMultiplier(_walkMeleeAttackSpeedMultiplier);
        SetRunMeleeAnimationSpeedMultiplier(_runMeleeAttackSpeedMultiplier);
        SetNormalDodgeAnimationSpeedMultiplier(_normalDodgeSpeedMultiplier);
        SetRunDodgeAnimationSpeedMultiplier(_runDodgeSpeedMultiplier);
    }

    public void SetMoveAnimationSpeedMultiplier(float multiplier) { _animator.SetFloat(_animatorHashMultiplierMove, multiplier); }

    public void SetIdleMeleeAnimationSpeedMultiplier(float multiplier) { _animator.SetFloat(_animatorHashMultiplierMove, multiplier); }

    public void SetWalkMeleeAnimationSpeedMultiplier(float multiplier) { _animator.SetFloat(_animatorHashMultiplierMove, multiplier); }

    public void SetRunMeleeAnimationSpeedMultiplier(float multiplier) { _animator.SetFloat(_animatorHashMultiplierMove, multiplier); }

    public void SetNormalDodgeAnimationSpeedMultiplier(float multiplier) { _animator.SetFloat(_animatorHashMultiplierMove, multiplier); }

    public void SetRunDodgeAnimationSpeedMultiplier(float multiplier) { _animator.SetFloat(_animatorHashMultiplierMove, multiplier); }

    public void SetAnimationValueVelocityX(float value) { _animator.SetFloat(_animatorHashVelocityX, value); }

    public void SetAnimationValueVelocityZ(float value) { _animator.SetFloat(_animatorHashVelocityZ, value); }

    public void SetAnimationValueIsMoving(bool value) { _animator.SetBool(_animatorHashIsMoving, value); }

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

    public bool IsAnimationPlaying() {return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f;}


    // =================================================================================================================================================
    //                                                              Animation Event Functions
    // =================================================================================================================================================
    private void AnimationEventDodgeStart() { _isDodging = true; }

    private void AnimationEventDodgeEnd() { _isDodging = false; }

    private void AnimationEventMeleeAttackStart() { _isMeleeAttacking = true; }

    private void AnimationEventMeleeAttackEnd() { _isMeleeAttacking = false; }
    // -------------------------------------------------------------------------------------------------------------------------------------------------
}
