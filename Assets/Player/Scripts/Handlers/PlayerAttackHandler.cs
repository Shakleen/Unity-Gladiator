using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    private const float THRESH = 1e-5f;
    [SerializeField] private Player _player;

    [SerializeField] private int _comboCounter;
    [SerializeField] private float _comboTimer;
    [SerializeField] private bool _attackStarted = false;
    [SerializeField] private bool _attacking = false;
    [SerializeField] private bool _attackEnded = false;
    
    public bool IsAttacking { get => _attacking; }
    
    public bool InAttackState() => _attackStarted || _attacking || _attackEnded;

    private void OnEnable() => PlayerInputHandler.OnAttackPressed += RequestQueueNextAttack;

    private void OnDisable() => PlayerInputHandler.OnAttackPressed -= RequestQueueNextAttack;

    private void RequestQueueNextAttack()
    {
        if (IsFirstAttack() || CanInitiateNextAttack())
        {
            _comboTimer = _player.Config.misc.idleAttackComboMaxTime;
            _comboCounter++;
            if (_comboCounter > 4) _comboCounter = 4;
        }
    }

    private bool CanInitiateNextAttack()
    {
        bool hasStamina = _attackEnded && !_player.StatusHandler.Stamina.IsEmpty();
        return !IsNextAttackQueued() && IsWithInComboTime() && hasStamina;
    }

    private void Start() => ResetCombo();

    private void ResetCombo() 
    {
        _comboTimer = _comboCounter = 0;
        _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(false);
        _player.AnimatorHandler.ResetMeleeAttackNumber();
        SetAttackBools();
    }

    private void Update() => DecreaseTimer();

    public void Attack()
    {
        if (IsNextAttackQueued())
        {
            _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(true);
            _player.AnimatorHandler.IncrementMeleeAttackNumber();
        }
    }

    private void DecreaseTimer()
    {
        if (_comboTimer > THRESH)
            _comboTimer -= Time.deltaTime;
        else
            ResetCombo();
    }

    private bool IsFirstAttack() => _comboCounter == 0;

    public bool IsNextAttackQueued() => _comboCounter > _player.AnimatorHandler.MeleeAttackNumber;

    private bool IsWithInComboTime() => _comboTimer >= THRESH;

    public void SetAttackBools(bool started = false, bool happening = false, bool ended = false)
    { 
        _attackStarted = started; 
        _attacking = happening;
        _attackEnded = ended;
    }
}
