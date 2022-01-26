using UnityEngine;

[CreateAssetMenu(fileName = "Sounds", menuName = "Gladiator/Sounds", order = 0)]
public class Sounds : ScriptableObject 
{
    [SerializeField] private AudioClip[] _footStepSounds;
    public AudioClip GetRandomFootStepSound() => _footStepSounds[UnityEngine.Random.Range(0, _footStepSounds.Length-1)];
    
    [SerializeField] private AudioClip[] _painSounds;
    public AudioClip GetRandomPainSound() => _painSounds[UnityEngine.Random.Range(0, _painSounds.Length-1)];

    [SerializeField] private AudioClip _deathSound;
    public AudioClip DeathSound { get => _deathSound; }
}