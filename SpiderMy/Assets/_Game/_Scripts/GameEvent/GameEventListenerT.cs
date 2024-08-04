using _Game.Scripts.Event;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[System.Serializable]
public class GameEventListenerT<T> 
{
    [FormerlySerializedAs("Event")] [Tooltip("Event to register with.")] public GameEventT<T> eventT;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent<T> Response;

    public void OnEnable()
    {
        eventT.RegisterListener(this);
    }

    public void OnDisable()
    {
        eventT.UnregisterListener(this);
    }

    public void OnEventRaised(T t)
    {
        Response.Invoke(t);
    }
}