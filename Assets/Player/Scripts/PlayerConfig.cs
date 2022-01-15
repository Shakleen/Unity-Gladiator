using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Gladiator/PlayerConfig", order = 0)]
public class PlayerConfig : ScriptableObject
{
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [Header("Walk Variables")]
    
    [Tooltip("Maximum forward veloctiy the player can walk")] 
    [SerializeField] [Range(0.5f, 10.0f)] private float _maxWalkVelocity = 2.0f;
    
    [Tooltip("Accelaration used to go towards maximum walk velocity")] 
    [SerializeField] [Range(0.5f, 10.0f)] private float _accelarationWalk = 4.0f;
    
    [Tooltip("Deceleration used to go towards idle or zero velocity")] 
    [SerializeField] [Range(0.5f, 10.0f)] private float _decelarationWalk = 6.0f;
    // -------------------------------------------------------------------------------------------------------------------------------------------------

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [Header("Run Variables")]
    
    [Tooltip("Maximum veloctiy the player can run")] 
    [SerializeField] [Range(0.5f, 20.0f)] private float _maxVelocityRun = 8.0f;
    
    [Tooltip("Accelaration used to go towards maximum run velocity")] 
    [SerializeField] [Range(0.5f, 20.0f)] private float _accelarationRun = 4.0f;
    
    [Tooltip("Deceleration used to go towards max walk velocity")] 
    [SerializeField] [Range(0.5f, 20.0f)] private float _decelarationRun = 6.0f;
    // -------------------------------------------------------------------------------------------------------------------------------------------------

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [Header("Camera variables")]
    [Tooltip("Camera movement sensitivity")] 
    [SerializeField] [Range(1f, 10.0f)] private float _cameraSensitivity = 5.0f;
    // -------------------------------------------------------------------------------------------------------------------------------------------------

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [Header("Player attack variables")]
    [Tooltip("Melee attack combo time limit")] 
    [SerializeField] [Range(1f, 10.0f)] private float _meleeAttackComboTimeLimit = 2.0f;
    // -------------------------------------------------------------------------------------------------------------------------------------------------

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [Header("Player stats")]
    [Tooltip("Player starting health")]
    [SerializeField] [Range(1.0f, 1000.0f)] private float _startingHealth = 100.0f;
    
    [Tooltip("Player health regen rate")]
    [SerializeField] [Range(1.0f, 10.0f)] private float _healthRegenPerSec = 10.0f;
    
    [Tooltip("Player starting stamina")]
    [SerializeField] [Range(1.0f, 100.0f)] private float _startingStamina = 50.0f;
    
    [Tooltip("Player stamina regen rate")]
    [SerializeField] [Range(1.0f, 10.0f)] private float _staminaRegenPerSec = 10.0f;
    
    [Tooltip("Player stamina depletion rate when running")]
    [SerializeField] [Range(1.0f, 20.0f)] private float _staminaDepletePerSec = 20.0f;
    
    [Tooltip("Player starting mana")]
    [SerializeField] [Range(1.0f, 100.0f)] private float _startingMana = 25.0f;
    
    [Tooltip("Player mana regen rate")]
    [SerializeField] [Range(1.0f, 10.0f)] private float _manaRegenPerSec = 5.0f;
    
    [Tooltip("Player mana depletion rate when using skills")]
    [SerializeField] [Range(1.0f, 20.0f)] private float _manaDepletePerSec = 10.0f;
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [Header("Player Action stamina costs")]

    [Tooltip("Dodge stamina cost")] 
    [SerializeField] [Range(1f, 100.0f)] private float _dodgeAttackStaminaCost = 5.0f;

    [Tooltip("Idle melee attack stamina cost")] 
    [SerializeField] [Range(1f, 100.0f)] private float _idleMeleeAttackStaminaCost = 5.0f;

    [Tooltip("Walking melee attack stamina cost")] 
    [SerializeField] [Range(1f, 100.0f)] private float _walkingMeleeAttackStaminaCost = 15.0f;

    [Tooltip("Running melee attack stamina cost")] 
    [SerializeField] [Range(1f, 100.0f)] private float _runningMeleeAttackStaminaCost = 25.0f;
    // -------------------------------------------------------------------------------------------------------------------------------------------------

    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [Header("Animation speed multipliers")]
    
    [Tooltip("Walk animation speed multiplier")] 
    [SerializeField] [Range(0.5f, 5.0f)] private float _moveSpeedMultiplier = 1.0f;
    
    [Tooltip("Idle Melee animation speed multiplier")] 
    [SerializeField] [Range(0.5f, 5.0f)] private float _idleMeleeAttackSpeedMultiplier = 1.35f;

    [Tooltip("Walk Melee animation speed multiplier")] 
    [SerializeField] [Range(0.5f, 5.0f)] private float _walkMeleeAttackSpeedMultiplier = 1.25f;
    
    [Tooltip("Run Melee animation speed multiplier")] 
    [SerializeField] [Range(0.5f, 5.0f)] private float _runMeleeAttackSpeedMultiplier = 1.15f;
    
    [Tooltip("Normal dodge animation speed multiplier")] 
    [SerializeField] [Range(0.5f, 5.0f)] private float _normalDodgeSpeedMultiplier = 1.75f;
    
    [Tooltip("Run dodge animation speed multiplier")] 
    [SerializeField] [Range(0.5f, 5.0f)] private float _runDodgeSpeedMultiplier = 1.5f;
    // -------------------------------------------------------------------------------------------------------------------------------------------------

    public float MaxWalkVelocity { get { return _maxWalkVelocity; } }
    public float AccelarationWalk { get { return _accelarationWalk; } }
    public float DecelarationWalk { get { return _decelarationWalk; } }
    public float MaxVelocityRun { get { return _maxVelocityRun; } }
    public float AccelarationRun { get { return _accelarationRun; } }
    public float DecelarationRun { get { return _decelarationRun; } }
    public float CameraSensitivity { get { return _cameraSensitivity; } }
    public float StartingHealth { get { return _startingHealth; } }
    public float HealthRegenPerSec { get { return _healthRegenPerSec; } }
    public float StartingStamina { get { return _startingStamina; } }
    public float StaminaRegenPerSec { get { return _staminaRegenPerSec; } }
    public float StaminaDepletePerSec { get { return _staminaDepletePerSec; } }
    public float StartingMana { get { return _startingMana; } }
    public float ManaRegenPerSec { get { return _manaRegenPerSec; } }
    public float ManaDepletePerSec { get { return _manaDepletePerSec; } }
    public float DodgeAttackStaminaCost { get { return _dodgeAttackStaminaCost; } }
    public float IdleMeleeAttackStaminaCost { get { return _idleMeleeAttackStaminaCost; } }
    public float WalkingMeleeAttackStaminaCost { get { return _walkingMeleeAttackStaminaCost; } }
    public float RunningMeleeAttackStaminaCost { get { return _runningMeleeAttackStaminaCost; } }
    public float MeleeAttackComboTimeLimit { get { return _meleeAttackComboTimeLimit; } }
    public float MoveSpeedMultiplier { get { return _moveSpeedMultiplier; } }
    public float IdleMeleeAttackSpeedMultiplier { get { return _idleMeleeAttackSpeedMultiplier; } }
    public float WalkMeleeAttackSpeedMultiplier { get { return _walkMeleeAttackSpeedMultiplier; } }
    public float RunMeleeAttackSpeedMultiplier { get { return _runMeleeAttackSpeedMultiplier; } }
    public float NormalDodgeSpeedMultiplier { get { return _normalDodgeSpeedMultiplier; } }
    public float RunDodgeSpeedMultiplier { get { return _runDodgeSpeedMultiplier; } }
}
