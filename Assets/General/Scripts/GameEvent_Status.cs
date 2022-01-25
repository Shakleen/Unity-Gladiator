using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent_Status", menuName = "Gladiator/Game Event/Status", order = 0)]
public class GameEvent_Status : ScriptableObject 
{
    private List<GameEventListener_Status> _listeners = new List<GameEventListener_Status>();

    public void Raise(Status status)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].OnEventRaised(status);
    }

    public void Register(GameEventListener_Status listener) => _listeners.Add(listener);

    public void Unregister(GameEventListener_Status listener) => _listeners.Remove(listener);
}