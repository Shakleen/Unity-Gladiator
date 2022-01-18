using UnityEngine;

[CreateAssetMenu(fileName = "AnimationSpeedConfig", menuName = "Gladiator/AnimationSpeedConfig", order = 0)]
public class AnimationSpeedConfig : ScriptableObject {
    [Tooltip("Movement animation speed multiplier")] [SerializeField] private float _move;
    public float move { get { return _move; } }
    
    [Tooltip("Idle Melee animation speed multiplier")] [SerializeField] private float _idleMeleeAttack;
    public float idleMeleeAttack { get { return _idleMeleeAttack; } }

    [Tooltip("Walk Melee animation speed multiplier")] [SerializeField] private float _walkMeleeAttack;
    public float walkMeleeAttack { get { return _walkMeleeAttack; } }
    
    [Tooltip("Run Melee animation speed multiplier")] [SerializeField] private float _runMeleeAttack;
    public float runMeleeAttack { get { return _runMeleeAttack; } }
    
    [Tooltip("Normal dodge animation speed multiplier")] [SerializeField]private float _normalDodge;
    public float normalDodge { get { return _normalDodge; } }
    [Tooltip("Run dodge animation speed multiplier")] [SerializeField] private float _runDodge;
    public float runDodge { get { return _runDodge; } }
}