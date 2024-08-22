using System;
using _Game.Scripts.Event;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    public class TimerMission : MonoBehaviour
    {
        public GameObject Clock;
        public TextMeshProUGUI textTime;
        public MissionManager missionManager;
        
        public GameEventListener onStart;
        public GameEventListener onComplete;
        
        public GameEvent outOfTime;
        private ShippingMissionSO shipMission;
        public bool run;
        private float currentTime;

        private void OnEnable()
        {
            onStart.OnEnable();
            onComplete.OnEnable();
        }
        
        private void OnDisable()
        {
            onStart.OnDisable();
            onComplete.OnDisable();
        }
        
        private void Update()
        {
            if (run)
            {
                if (currentTime >= 0)
                {
                    currentTime -= Time.deltaTime;
                    textTime.text = currentTime.ToString("F1");
                }
                else
                {
                    outOfTime.Raise();
                    Clock.SetActive(false);
                }
            }
            else
            {
                Clock.SetActive(false);
            }
        }

        public void StartMission()
        {
            if (missionManager.mainMissionSO.GetCurrentMission().missionType == MissionType.Delivery)
            {
                currentTime = missionManager.mainMissionSO.GetCurrentMission().timeLimit;
                Clock.SetActive(true);
                run = true;
            }
        }
        
        public void StopTimer()
        {
            run = false;
        }
    }
}