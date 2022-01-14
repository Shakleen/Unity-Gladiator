using UnityEngine;

[CreateAssetMenu(fileName = "AIConfig", menuName = "Gladiator/AIConfig", order = 1)]
public class AIConfig : ScriptableObject {
    
    [Header("Path update parameters")]
    [Tooltip("Minimum amount of time to pass before the next path update is called.")]
    [SerializeField] [Range(0.2f, 10.0f)] private float _minimumUpdateWaitTime = 1.0f;
    
    [Tooltip("Minimum amount of units player should travel in order to update path.")]
    [SerializeField] [Range(1.0f, 10.0f)] private float _minimumUpdateDistance = 5.0f;

    public float MinimumUpdateWaitTime { get { return _minimumUpdateWaitTime; } }
    public float MinimumUpdateDistance { get { return _minimumUpdateDistance; } }
    
}