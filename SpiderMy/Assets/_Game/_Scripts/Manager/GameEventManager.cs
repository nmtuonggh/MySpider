using System;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Manager
{
    public class GameEventManager : MonoBehaviour
    {
        public static GameEventManager instance { get; private set; }

        public QuestEvent questEvent;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Found more than 1 GameEventManager in the scene");
            }
            instance = this;
            
            questEvent = new QuestEvent();
        }
    }
}