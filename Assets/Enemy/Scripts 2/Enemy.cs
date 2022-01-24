using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyInteractionHandler _interactionHandler;
    [SerializeField] private EnemyAnimationHandler _animationHandler;
    public EnemyAnimationHandler AnimationHandler { get => _animationHandler; }

    [SerializeField] private StatusConfig _health;
    public StatusConfig Health { get => _health; }
}
