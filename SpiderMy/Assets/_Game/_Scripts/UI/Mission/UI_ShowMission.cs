using System.Collections.Generic;
using _Game.Scripts.Event;
using TMPro;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    public class UI_ShowMission : MonoBehaviour
    {
        [SerializeField] private MissionManager missionManager;
        [SerializeField] private GameObject MissionUI;
        [SerializeField] private BlackBoard blackBoard;

        [Header("==========Mission UI==========")] [SerializeField]
        private TextMeshProUGUI currentMissionName;

        [SerializeField] private TextMeshProUGUI step1Mission;
        [SerializeField] private TextMeshProUGUI step2Mission;

        [SerializeField] private GameObject step1Done;
        [SerializeField] private GameObject step2Item;

        private BaseMissionSO currentMission;

        [Header("==========Mission Ship==========")]
        public GameObject deliveryCountUI;

        public List<GameObject> deliveryDoneUIs;
        private int _currentDeliveryDone;

        [Header("==========Mission Event Listener==========")]
        public GameEventListener onMissionStart;

        public GameEventListener onMissionUpdate;
        public GameEventListener onMissionComplete;
        public GameEventListener onMissionFail;
        public GameEventListener inMissionRange;
        public GameEventListener inPickupDelivery;

        private void OnEnable()
        {
            onMissionStart.OnEnable();
            onMissionUpdate.OnEnable();
            onMissionComplete.OnEnable();
            onMissionFail.OnEnable();
            inMissionRange.OnEnable();
            inPickupDelivery.OnEnable();
        }

        private void OnDisable()
        {
            onMissionStart.OnDisable();
            onMissionUpdate.OnDisable();
            onMissionComplete.OnDisable();
            onMissionFail.OnDisable();
            inMissionRange.OnDisable();
            inPickupDelivery.OnDisable();
        }

        public void HandlerMissionStart()
        {
            currentMission = missionManager.mainMissionSO.GetCurrentMission();
            step2Item.SetActive(false);
            currentMissionName.text = "Mission " + currentMission.missionType.ToString();
            step1Mission.text = "Go to mission location";
            MissionUI.SetActive(true);
            if (currentMission.missionType == MissionType.Delivery)
            {
                _currentDeliveryDone = 0;
                foreach (var child in deliveryDoneUIs)
                {
                    child.gameObject.SetActive(false);
                }
            }
            
        }

        public void HandlerInRangeMission()
        {
            Debug.Log("run");
            step2Item.SetActive(true);
            switch (currentMission.missionType)
            {
                case MissionType.Fighting:
                    step2Mission.text = "Kill all enemies";
                    break;
                case MissionType.Delivery:
                    blackBoard.Bag.SetActive(true);
                    deliveryCountUI.SetActive(true);
                    step2Mission.text = "Deliver the package to all customers";
                    break;
                case MissionType.Protect:
                    step2Mission.text = "Rescue the hostages";
                    break;
            }
        }

        public void HandlerMissionComplete()
        {
            deliveryCountUI.SetActive(false);
            blackBoard.Bag.SetActive(false);
            MissionUI.SetActive(false);
        }

        public void HandlerMissionFail()
        {
            //TODO: Fix this
            deliveryCountUI.SetActive(false);
            blackBoard.Bag.SetActive(false);
            MissionUI.SetActive(false);
            MissionUI.SetActive(true);
            currentMission = missionManager.mainMissionSO.GetCurrentMission();
            step2Item.SetActive(false);
            currentMissionName.text = "Mission " + currentMission.missionType.ToString();
            step1Mission.text = "Go to mission location";
        }

        public void HandlerInPickupDelivery()
        {
            deliveryDoneUIs[_currentDeliveryDone].SetActive(true);
            _currentDeliveryDone++;
        }
    }
}