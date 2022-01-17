using UnityEngine;

[CreateAssetMenu(fileName = "AttackConfig", menuName = "Gladiator/AttackConfig", order = 0)]
public class AttackConfig : ScriptableObject
{
    [Tooltip("Melee attack combo time limit")] [SerializeField] private float _idleAttackComboMaxTime;
    public float idleAttackComboMaxTime { get { return _idleAttackComboMaxTime; } }

    [Tooltip("Max combos in idle attack")] [SerializeField] private int _maxCombos;
    public int maxCombos { get { return _maxCombos; } }
}
