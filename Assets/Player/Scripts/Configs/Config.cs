using UnityEngine;

public class Config : MonoBehaviour 
{
    [SerializeField] public MoveConfig walk;
    [SerializeField] public MoveConfig run;

    [SerializeField] public StatusConfig health;
    [SerializeField] public StatusConfig stamina;
    [SerializeField] public StatusConfig mana;

    [SerializeField] public AnimationSpeedConfig animationSpeed;

    [SerializeField] public StaminaCost staminaCost;
    
    [SerializeField] public PlayerConfig misc;
}