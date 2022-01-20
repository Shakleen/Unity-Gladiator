using UnityEngine;

public class AIInteractionHandler : MonoBehaviour
{
    [SerializeField] private AIAgent _aiAgent;
    [SerializeField] private BarManager _healthBar;
    private float _cumulativeDamage;

    private void Start() => _healthBar.InitializeBar(_aiAgent.Health);

    public void TakeDamage(float damage)
    {
        _aiAgent.Health.Take(damage);
        _healthBar.UpdateBar(_aiAgent.Health);
    }
}
