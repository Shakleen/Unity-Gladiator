using UnityEngine;

public class ConfigHandler : MonoBehaviour 
{
    [SerializeField] public MoveConfig walk;
    [SerializeField] public MoveConfig run;

    [SerializeField] public StatusConfig health;
    [SerializeField] public StatusConfig stamina;

    [SerializeField] public AnimationSpeedConfig animationSpeed;

    [SerializeField] public StaminaCost staminaCost;
    
    [SerializeField] public PlayerConfig misc;
    [SerializeField] public AttackConfig attack;

    [SerializeField] public Sounds sounds;
}