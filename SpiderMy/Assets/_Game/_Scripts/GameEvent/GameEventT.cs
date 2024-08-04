using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Event
{
    [Serializable]
    public class GameEventT<T> : ScriptableObject
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        [SerializeField] private List<GameEventListenerT<T>> eventListeners = 
            new List<GameEventListenerT<T>>();

        public void Raise(T t)
        {
            for(int i = eventListeners.Count -1; i >= 0; i--)
                eventListeners[i].OnEventRaised(t);
        }

        public void RegisterListener(GameEventListenerT<T> listenerT)
        {
            if (!eventListeners.Contains(listenerT))
                eventListeners.Add(listenerT);
        }

        public void UnregisterListener(GameEventListenerT<T> listenerT)
        {
            if (eventListeners.Contains(listenerT))
                eventListeners.Remove(listenerT);
        }
    }
}