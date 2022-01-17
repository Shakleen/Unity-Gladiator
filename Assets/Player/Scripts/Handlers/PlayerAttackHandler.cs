using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    private const float THRESH = 1e-5f;
    [SerializeField] private Player _player;

    [SerializeField] private int _comboCounter;
    [SerializeField] private float _comboTimer;
    [SerializeField] private bool _attackStarted;
    [SerializeField] private bool _attacking;
    [SerializeField] private bool _attackEnded;
    
    public bool IsAttacking { get => _attacking; }
    
    public bool InAttackState() => _attackStarted || _attacking || _attackEnded;

    private void OnEnable() => PlayerInputHandler.OnAttackPressed += RequestQueueNextAttack;

    private void OnDisable() => PlayerInputHandler.OnAttackPressed -= RequestQueueNextAttack;

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

    private void Start() => ResetCombo();

    public void ResetCombo() 
    {
        _comboTimer = _comboCounter = 0;
        _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(false);
        _player.AnimatorHandler.ResetMeleeAttackNumber();
        SetAttackBools();
    }

    public void Attack()
    {
        if (IsNextAttackQueued())
        {
            _player.AnimatorHandler.SetAnimationValueIsMeleeAttacking(true);
            _player.AnimatorHandler.IncrementMeleeAttackNumber();
        }
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
}
