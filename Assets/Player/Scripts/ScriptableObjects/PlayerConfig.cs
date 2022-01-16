using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Gladiator/PlayerConfig", order = 0)]
public class PlayerConfig : ScriptableObject
{
    [Header("Camera variables")]
    [Tooltip("Camera movement sensitivity")] 
    [SerializeField] [Range(1f, 10.0f)] private float _cameraSensitivity = 5.0f;
    public float cameraSensitivity { get { return _cameraSensitivity; } }

    [Header("Player attack variables")]
    [Tooltip("Melee attack combo time limit")] 
    [SerializeField] [Range(1f, 10.0f)] private float _idleAttackComboMaxTime = 2.0f;
    public float idleAttackComboMaxTime { get { return _idleAttackComboMaxTime; } }

    [Header("Player stats")]
    [Tooltip("Regenaration delayed upon use")]
    [SerializeField] [Range(0.1f, 3.0f)] private float _regenDelay = 0.5f;
    public float regenDelay { get { return _regenDelay; } }
}
