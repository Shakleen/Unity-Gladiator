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

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    // Variables
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    private bool _isDodging = false;

    // =================================================================================================================================================
    //                                                              Getters and Setters
    // =================================================================================================================================================
    public Animator Animator { get { return _animator; } }
    public bool IsDodging { get { return _isDodging; } }


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
    }

    public void SetAnimationValueVelocityX(float value) { _animator.SetFloat(_animatorHashVelocityX, value); }

    public void SetAnimationValueVelocityZ(float value) { _animator.SetFloat(_animatorHashVelocityZ, value); }

    public void SetAnimationValueIsRunning(bool value) { _animator.SetBool(_animatorHashIsRunning, value); }

    public void SetAnimationValueIsDodging(bool value) { _animator.SetBool(_animatorHashIsDodging, value); }


    // =================================================================================================================================================
    //                                                              Animation Event Functions
    // =================================================================================================================================================
    private void AnimationEventDodgeStart() { _isDodging = true; }

    private void AnimationEventDodgeEnd() { _isDodging = false; }
    // -------------------------------------------------------------------------------------------------------------------------------------------------
}
