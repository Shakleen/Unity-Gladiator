using System;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    #region Events
    public static event Action OnAttackSlashStart;
    public static event Action OnAttackSlashEnd;
    #endregion

    #region Component references
    [SerializeField] private Animator _animator;
    [SerializeField] private Player _player;
    #endregion

    #region Animation paramter hash variables
    private int _hashVelocityX, _hashVelocityZ;
    private int _hashIsMoving, _hashIsRunning, _hashIsDodging, _hashIsMeleeAttacking;
    private int _hashMeleeAttackNumber;
    private int _hashMultiplierMove, _hashMultiplierDodgeNormal, _hashMultiplierDodgeRun;
    private int _hashMultiplierIdleMelee, _hashMultiplierWalkMelee, _hashMultiplierRunMelee;
    #endregion

    #region Member variables
    private bool _isDodging = false;
    private int _meleeAttackNumber = 0;
    #endregion

    #region Getters
    public bool IsDodging { get => _isDodging; }
    public int MeleeAttackNumber { get => _meleeAttackNumber; }
    #endregion

    private void Awake() 
    {
        _hashVelocityX = Animator.StringToHash("velocityX");
        _hashVelocityZ = Animator.StringToHash("velocityZ");
        _hashIsMoving = Animator.StringToHash("isMoving");
        _hashIsRunning = Animator.StringToHash("isRunning");
        _hashIsDodging = Animator.StringToHash("isDodging");
        _hashIsMeleeAttacking = Animator.StringToHash("isMeleeAttacking");
        _hashMeleeAttackNumber = Animator.StringToHash("meleeAttackNumber");
        _hashMultiplierMove = Animator.StringToHash("moveSpeedMultiplier");
        _hashMultiplierIdleMelee = Animator.StringToHash("idleMeleeAttackSpeedMultiplier");
        _hashMultiplierWalkMelee = Animator.StringToHash("walkMeleeAttackSpeedMultiplier");
        _hashMultiplierRunMelee = Animator.StringToHash("runMeleeAttackSpeedMultiplier");
        _hashMultiplierDodgeNormal = Animator.StringToHash("normalDodgeSpeedMultiplier");
        _hashMultiplierDodgeRun = Animator.StringToHash("runDodgeSpeedMultiplier");
    }

    private void OnEnable()
    {
        SetMultipliers();
        PlayerMovementHandler.OnCurrentMovementChange += OnCurrentMovementChange;
        PlayerInputHandler.OnRunPressed += OnRunPressed;
        PlayerInputHandler.OnMovePressed += OnMovePressed;
        PlayerInputHandler.OnDodgePressed += OnDodgePressed;
    }

    private void SetMultipliers()
    {
        SetMoveAnimationSpeedMultiplier(_player.Config.animationSpeed.move);
        SetIdleMeleeAnimationSpeedMultiplier(_player.Config.animationSpeed.idleMeleeAttack);
        SetWalkMeleeAnimationSpeedMultiplier(_player.Config.animationSpeed.walkMeleeAttack);
        SetRunMeleeAnimationSpeedMultiplier(_player.Config.animationSpeed.runMeleeAttack);
        SetNormalDodgeAnimationSpeedMultiplier(_player.Config.animationSpeed.normalDodge);
        SetRunDodgeAnimationSpeedMultiplier(_player.Config.animationSpeed.runDodge);
    }

    private void OnDisable() 
    {
        PlayerMovementHandler.OnCurrentMovementChange -= OnCurrentMovementChange;
        PlayerInputHandler.OnRunPressed -= OnRunPressed;
        PlayerInputHandler.OnMovePressed -= OnMovePressed;
        PlayerInputHandler.OnDodgePressed -= OnDodgePressed;
    }

    private void OnCurrentMovementChange()
    {
        SetAnimationValueVelocityX(_player.MovementHandler.CurrentMovementVelocity.x);
        SetAnimationValueVelocityZ(_player.MovementHandler.CurrentMovementVelocity.z);
    }

    #region Input Event Callbacks
    private void OnRunPressed() => SetAnimationValueIsRunning(_player.InputHandler.IsInputActiveRun);
    private void OnMovePressed() => SetAnimationValueIsMoving(_player.InputHandler.IsInputActiveMovement);
    private void OnDodgePressed() => SetAnimationValueIsDodging(_player.InputHandler.IsInputActiveDodge);
    #endregion

    #region Animation speed multiplier setters
    public void SetMoveAnimationSpeedMultiplier(float multiplier) => _animator.SetFloat(_hashMultiplierMove, multiplier);
    public void SetIdleMeleeAnimationSpeedMultiplier(float multiplier) => _animator.SetFloat(_hashMultiplierIdleMelee, multiplier);
    public void SetWalkMeleeAnimationSpeedMultiplier(float multiplier) => _animator.SetFloat(_hashMultiplierWalkMelee, multiplier);
    public void SetRunMeleeAnimationSpeedMultiplier(float multiplier) => _animator.SetFloat(_hashMultiplierRunMelee, multiplier);
    public void SetNormalDodgeAnimationSpeedMultiplier(float multiplier) => _animator.SetFloat(_hashMultiplierDodgeNormal, multiplier);
    public void SetRunDodgeAnimationSpeedMultiplier(float multiplier) => _animator.SetFloat(_hashMultiplierDodgeRun, multiplier);
    #endregion

    #region Animation parameter setters
    private void SetAnimationValueVelocityX(float value) => _animator.SetFloat(_hashVelocityX, value);
    private void SetAnimationValueVelocityZ(float value) => _animator.SetFloat(_hashVelocityZ, value);
    private void SetAnimationValueIsMoving(bool value) => _animator.SetBool(_hashIsMoving, value);
    private void SetAnimationValueIsRunning(bool value) => _animator.SetBool(_hashIsRunning, value);
    private void SetAnimationValueIsDodging(bool value) => _animator.SetBool(_hashIsDodging, value);
    public void SetAnimationValueIsMeleeAttacking(bool value) => _animator.SetBool(_hashIsMeleeAttacking, value);
    public void IncrementMeleeAttackNumber()
    {
        _meleeAttackNumber++;
        _animator.SetInteger(_hashMeleeAttackNumber, _meleeAttackNumber);
    }
    public void ResetMeleeAttackNumber() 
    { 
        _meleeAttackNumber = 0; 
        _animator.SetInteger(_hashMeleeAttackNumber, _meleeAttackNumber);
    }
    #endregion

    public bool IsAnimationPlaying() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f;

    #region Animation Events

    #region Dodge events
    private void AnimationEventDodgeStart() 
    {
        _isDodging = true;
        _player.StatusHandler.UseStamina(_player.Config.staminaCost.dodge);
    }
    private void AnimationEventDodgeEnd() => _isDodging = false;
    #endregion

    #region Melee attack events
    private void AnimationEventMeleeAttackStart() => _player.AttackHandler.SetAttackBools(started : true);
    private void AnimationEventMeleeAttacking() 
    {
        _player.AttackHandler.SetAttackBools(happening : true);
        _player.StatusHandler.UseStamina(_player.Config.staminaCost.idleMeleeAttack);
        OnAttackSlashStart?.Invoke();
    }
    private void AnimationEventMeleeAttackEnd() 
    {
        _player.AttackHandler.SetAttackBools(ended : true);
        OnAttackSlashEnd?.Invoke();
    }
    #endregion

    #endregion
    // -------------------------------------------------------------------------------------------------------------------------------------------------
}
