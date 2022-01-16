using UnityEngine;

[CreateAssetMenu(fileName = "StaminaCost", menuName = "Gladiator/StaminaCost", order = 0)]
public class StaminaCost : ScriptableObject {
    [Tooltip("Dodge stamina cost")] [SerializeField] private float _dodge;
    public float dodge { get { return _dodge; } }

    [Tooltip("Idle melee attack stamina cost")] [SerializeField] private float _idleMeleeAttack;
    public float idleMeleeAttack { get { return _idleMeleeAttack; } }

    [Tooltip("Walking melee attack stamina cost")] [SerializeField] private float _walkingMeleeAttack;
    public float walkingMeleeAttack { get { return _walkingMeleeAttack; } }

    [Tooltip("Running melee attack stamina cost")] [SerializeField] private float _runningMeleeAttack;
    public float runningMeleeAttack { get { return _runningMeleeAttack; } }
}