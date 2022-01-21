using UnityEngine;

public class RagDollHandler : MonoBehaviour
{
    [SerializeField] private AIAgent _aiAgent;
    private Rigidbody[] _rigidBodies;
    private bool _isRagDollActive = false;

    public bool IsRagDollActive { get => _isRagDollActive; }

    private void Awake() 
    { 
        _rigidBodies = GetComponentsInChildren<Rigidbody>(); 
        AddHitBoxHandler();
    }

    public void SetRagDollStatus(bool status)
    {
        _isRagDollActive = status;

        foreach(Rigidbody rigidbody in _rigidBodies)
            rigidbody.isKinematic = !status;
        
        _aiAgent.AnimationHandler.SetAnimatorActiveStatus(!status);
    }

    private void AddHitBoxHandler()
    {
        foreach(Rigidbody rigidbody in _rigidBodies)
            rigidbody.gameObject.AddComponent<HitBoxHandler>();
    }
}
