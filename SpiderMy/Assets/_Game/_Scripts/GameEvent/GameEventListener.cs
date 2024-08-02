using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Event
{
    [Serializable]
    public class GameEventListener
    {
        [Tooltip("Event to register with.")]
        public GameEvent Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent Response;

        public void OnEnable()
        {
            Event.RegisterListener(this);
        }

        public void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}