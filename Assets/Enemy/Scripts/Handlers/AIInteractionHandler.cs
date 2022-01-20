using UnityEngine;

public class AIInteractionHandler : MonoBehaviour
{
    [SerializeField] private AIAgent _aiAgent;
    [SerializeField] private BarManager _healthBar;
    [SerializeField] private MeleeWeaponHandler _weapon;
    private float _cumulativeDamage;

    private void OnEnable()
    {
        SetActiveHealthBarUI(true);
        SetWeaponGravity(false);
        AIAgent.OnDeath += HandleOnDeath;
    }

    private void OnDisable() => AIAgent.OnDeath -= HandleOnDeath;

    private void Start() => _healthBar.InitializeBar(_aiAgent.Health);

    public void SetWeaponDamageStatus(bool status) => _weapon.canDamage = status;

    public void TakeDamage(float damage)
    {
        _aiAgent.Health.Take(damage);
        _healthBar.UpdateBar(_aiAgent.Health);
    }

    #region Handling Death
    private void HandleOnDeath()
    {
        SetActiveHealthBarUI(false);
        SetWeaponGravity(true);
    }

    public void SetActiveHealthBarUI(bool status) 
    {
        Canvas canvas = _healthBar.GetComponentInParent<Canvas>();
        canvas.gameObject.SetActive(status);
    }

    private void SetWeaponGravity(bool status)
    {
        _weapon.GetComponent<Rigidbody>().useGravity = status;
        _weapon.GetComponent<BoxCollider>().isTrigger = !status;
    }
    #endregion
}
