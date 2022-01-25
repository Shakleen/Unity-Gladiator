using UnityEngine;
using UnityEngine.Events;

public class GameEventListener_Status : MonoBehaviour 
{
    [SerializeField] private GameEvent_Status Event;
    [SerializeField] private UnityEvent<Status> Response;
    
    public void OnEventRaised(Status status) => Response.Invoke(status);

    private void OnEnable() => Event.Register(this);

    private void OnDisable() => Event.Unregister(this);
}