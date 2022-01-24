using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour 
{
    [SerializeField] private GameEvent Event;
    [SerializeField] private UnityEvent Response;
    
    public void OnEventRaised() => Response.Invoke();

    private void OnEnable() => Event.Register(this);

    private void OnDisable() => Event.Unregister(this);
}