using UnityEngine;

public class UIEnemyHealth : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    private Camera _mainCamera;

    private void Start() => _mainCamera = Camera.main;

    private void LateUpdate() => transform.position = _mainCamera.WorldToScreenPoint(_target.position + _offset);
}
