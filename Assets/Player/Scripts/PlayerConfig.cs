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
    [SerializeField] [Range(0.5f, 20.0f)] private float _maxRunVelocity = 8.0f;
    
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

    private Movement _walk, _run;
    private Status _health, _stamina, _mana;
    private StaminaCost _staminaCost;
    private AnimationSpeedMultipliers _animationSpeedMultiplier;

    private void Awake() 
    {
        _walk = new Movement(_maxWalkVelocity, _accelarationWalk, _decelarationWalk);
        _run = new Movement(_maxRunVelocity, _accelarationRun, _decelarationRun);

        _health = new Status(_startingHealth, _healthRegenPerSec, 0);
        _stamina = new Status(_startingStamina, _staminaRegenPerSec, _staminaDepletePerSec);
        _mana = new Status(_startingMana, _manaRegenPerSec, _manaDepletePerSec);

        _staminaCost = new StaminaCost(
            _dodgeAttackStaminaCost,
            _idleMeleeAttackStaminaCost,
            _walkingMeleeAttackStaminaCost,
            _runningMeleeAttackStaminaCost
        );

        _animationSpeedMultiplier = new AnimationSpeedMultipliers(
            _moveSpeedMultiplier,
            _idleMeleeAttackSpeedMultiplier,
            _walkingMeleeAttackStaminaCost,
            _runDodgeSpeedMultiplier,
            _normalDodgeSpeedMultiplier,
            _runDodgeSpeedMultiplier
        );
    }

    public Movement walk { get { return walk; } }
    public Movement run { get { return run; } }
    public Status health { get { return health; } }
    public Status stamina { get { return stamina; } }
    public Status mana { get { return mana; } }
    public StaminaCost staminaCost { get { return staminaCost; } }
    public AnimationSpeedMultipliers animationSpeedMultiplier { get { return animationSpeedMultiplier; } }

    public float CameraSensitivity { get { return _cameraSensitivity; } }
    public float MeleeAttackComboTimeLimit { get; }
}

public class Movement
{
    public Movement(float maxVelocity, float accelaration, float decelaration)
    {
        this.maxVelocity = maxVelocity;
        this.accelaration = accelaration;
        this.decelaration = decelaration;
    }
    public float maxVelocity { get; }
    public float accelaration { get; }
    public float decelaration { get; }

}

public class Status
{
    public Status(float startingCapacity, float regenRate, float depleteRate)
    {
        this.startingCapacity = startingCapacity;
        this.regenPerSec = regenRate;
        this.depletePerSec = depleteRate;
    }

    public float startingCapacity { get; }
    public float regenPerSec { get; }
    public float depletePerSec { get; }
}

public class StaminaCost
{
    public StaminaCost(float dodge, float idleMelee, float walkingMelee, float runningMelee)
    {
        this.dodge = dodge;
        this.idleMelee = idleMelee;
        this.walkingMelee = walkingMelee;
        this.runningMelee = runningMelee;
    }
    public float dodge { get; }
    public float idleMelee { get; }
    public float walkingMelee { get; }
    public float runningMelee { get; }
}

public class AnimationSpeedMultipliers
{
    public AnimationSpeedMultipliers(
        float move,
        float idleMeleeAttack,
        float walkMeleeAttack,
        float runMeleeAttack,
        float normalDodge,
        float runDodge
    )
    {
        this.move = move;
        this.idleMeleeAttack = idleMeleeAttack;
        this.walkMeleeAttack = walkMeleeAttack;
        this.runMeleeAttack = runMeleeAttack;
        this.normalDodge = normalDodge;
        this.runDodge = runDodge;
    }
    
    public float move { get; }
    public float idleMeleeAttack { get; }
    public float walkMeleeAttack { get; }
    public float runMeleeAttack { get; }
    public float normalDodge { get; }
    public float runDodge { get; }
}