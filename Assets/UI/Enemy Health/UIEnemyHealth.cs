using UnityEngine;

public class UIEnemyHealth : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Enemy _owner;
    [SerializeField] private BarManager _bar;
    private Camera _mainCamera;

    private void Start() 
    {
        _mainCamera = Camera.main;
        _bar.UpdateBarValue(_owner.Health);
    }

    public void UpdateBar() => _bar.UpdateBarValue(_owner.Health);

    private void LateUpdate() => transform.position = _mainCamera.WorldToScreenPoint(_target.position + _offset);
}
