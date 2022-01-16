using UnityEngine;

[CreateAssetMenu(fileName = "MoveConfig", menuName = "Gladiator/MoveConfig", order = 0)]
public class MoveConfig : ScriptableObject 
{
    [Tooltip("Maximum velocity during action in any direction")] 
    [SerializeField] private float _maxVelocity;
    public float maxVelocity { get { return _maxVelocity; } }
    
    [Tooltip("Accelaration used to go towards maximum velocity")] 
    [SerializeField] private float _accelaration;
    public float accelaration { get { return _accelaration; } }
    
    [Tooltip("Deceleration used to go towards idle or zero velocity")] 
    [SerializeField] private float _decelaration;
    public float decelaration { get { return _decelaration; } }
}