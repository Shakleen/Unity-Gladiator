using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollHandler : MonoBehaviour
{
    [SerializeField] private AIAgent _aiAgent;
    private Rigidbody[] _rigidBodies;

    private void Awake() 
    { 
        _rigidBodies = GetComponentsInChildren<Rigidbody>(); 
        AddHitBoxHandler();
    }

    private void Start() { SetActiveRagDoll(true); }

    public void SetActiveRagDoll(bool isKinematic)
    {
        foreach(Rigidbody rigidbody in _rigidBodies)
            rigidbody.isKinematic = isKinematic;
        
        _aiAgent.AnimationHandler.SetAnimatorActiveStatus(isKinematic);
    }

    private void AddHitBoxHandler()
    {
        foreach(Rigidbody rigidbody in _rigidBodies)
            rigidbody.gameObject.AddComponent<HitBoxHandler>();
    }
}
