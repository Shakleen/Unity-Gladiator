using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAttackHandler : MonoBehaviour
{
    #region Variables
    private const float THRESH = 1e-5f;
    private Player _player;
    private MeleeWeaponHandler _weapon;

    [SerializeField] private int _comboCounter;
    [SerializeField] private float _comboTimer;
    [SerializeField] private bool _attackStarted;
    [SerializeField] private bool _attacking;
    [SerializeField] private bool _attackEnded;
    #endregion
    
    public bool IsAttacking { get => _attacking; }
    public bool IsAttackComplete() => !(_attackStarted || _attacking || _attackEnded);
    public bool NoAttackActivity() => !_player.InputHandler.IsInputActiveMeleeAttack && !_player.AttackHandler.IsAttacking;

    private void Awake() 
    {
        _player = GetComponent<Player>();
        _weapon = GetComponentInChildren<MeleeWeaponHandler>();    
    }

    private void OnEnable() 
    {
        PlayerInputHandler.OnAttackPressed += RequestQueueNextAttack;
        PlayerAnimationHandler.OnAttackSlashStart += OnSlashStart;
        PlayerAnimationHandler.OnAttackSlashEnd += OnSlashEnd;
    }

    private void OnDisable() 
    {
        PlayerInputHandler.OnAttackPressed -= RequestQueueNextAttack;
        PlayerAnimationHandler.OnAttackSlashStart += OnSlashStart;
        PlayerAnimationHandler.OnAttackSlashEnd += OnSlashEnd;
    }

    private void OnSlashStart() 
    {
        _weapon.SetTrailActive(true);
        _weapon.PlaySlashSound();
    }

    private void OnSlashEnd() 
    {
        _weapon.SetTrailActive(false);
        
        if (_comboCounter == _player.Config.attack.maxCombos)
        {
            _comboCounter = 0;
            _player.AnimatorHandler.ResetMeleeAttackNumber();
        }
    }

    #region Attacking queuing
    private void RequestQueueNextAttack()
    {
        if (IsFirstAttack() || CanInitiateNextAttack())
        {
            _comboTimer = _player.Config.attack.idleAttackComboMaxTime;
            
            if (_comboCounter >= _player.Config.attack.maxCombos) 
                _comboCounter = _player.Config.attack.maxCombos;
            else
                _comboCounter++;
        }
    }

    private bool IsFirstAttack() => _comboCounter == 0;

    private bool CanInitiateNextAttack()
    {
        bool hasStamina = !_player.StatusHandler.Stamina.IsEmpty();
        return _attackEnded && !IsNextAttackQueued() && IsWithInComboTime() && hasStamina;
    }

    private bool IsWithInComboTime() => _comboTimer >= THRESH;
    #endregion
    
    private void Start() => ResetCombo();

    public void ResetCombo() 
    {
        _comboTimer = _comboCounter = 0;
        _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(false);
        _player.AnimatorHandler.ResetMeleeAttackNumber();
        SetAttackBools();
        _weapon.canDamage = false;
    }

    public void Attack()
    {
        if (IsNextAttackQueued())
        {
            _player.AnimatorHandler.IncrementMeleeAttackNumber();
            _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(true);
            SetWeaponDamageMode(true);
        }
    }

    public void ChargeAttack()
    {
        _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(true);
        SetWeaponDamageMode(true);
    }
    
    public void DecreaseAttackComboTimer()
    {
        if (_comboTimer > THRESH)
            _comboTimer -= Time.deltaTime;
        else
            ResetCombo();
    }

    public bool IsNextAttackQueued() => _comboCounter > _player.AnimatorHandler.MeleeAttackNumber;

    public void SetAttackBools(bool started = false, bool happening = false, bool ended = false)
    { 
        _attackStarted = started; 
        _attacking = happening;
        _attackEnded = ended;
    }

    public void SetWeaponDamageMode(bool mode) => _weapon.canDamage = mode;
}
