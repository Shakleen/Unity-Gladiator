using UnityEngine;

public class EnemyCounter : MonoBehaviour 
{
    [SerializeField] private EnemyCountKeeper _countKeeper;
    [SerializeField] private Enemy _enemy;

    private void OnEnable() => _countKeeper.Register(_enemy as Enemy);

    private void OnDisable() => _countKeeper.Unregister(_enemy as Enemy);
}