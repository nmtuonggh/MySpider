using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Event;
using DG.Tweening;
using SFRemastered._Game._Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace SFRemastered._Game._Scripts.Mission
{
    public class UI_ShowMission : MonoBehaviour
    {
        [SerializeField] private MissionManager missionManager;
        [SerializeField] private GameObject MissionUI;
        [SerializeField] private BlackBoard blackBoard;
        [SerializeField] private PlayerDataSO playerData;
        [SerializeField] private TimeManager timeManager;
        private BaseMissionSO currentMission;

        [Header("==========Mission UI==========")] 
        [SerializeField] private TextMeshProUGUI currentMissionName;
        [SerializeField] private TextMeshProUGUI step1Mission;
        [SerializeField] private TextMeshProUGUI step2Mission;
        [SerializeField] private GameObject step1Done;
        [SerializeField] private GameObject step2Item;
        
        [Header("==========Mission Status==========")]
        [SerializeField] private GameObject CompleteUI;
        [SerializeField] private GameObject FailFightUI;
        [SerializeField] private GameObject FailShipUI;
        [SerializeField] private TextMeshProUGUI cashRewardValue;
        [SerializeField] private TextMeshProUGUI expRewardValue;
        [SerializeField] private Slider expSlider;
        [SerializeField] private TextMeshProUGUI currentLV;
        [SerializeField] private GameObject videoAds1;
        [SerializeField] private GameObject videoAds2;
        

        [Header("==========Mission Ship==========")]
        public GameObject deliveryCountUI;
        public List<GameObject> deliveryDoneUIs;
        public GameObject ClockUI;
        public TextMeshProUGUI textTime;
        private int _currentDeliveryDone;

        [Header("==========Mission Event ==========")]
        //public GameEvent g
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
            DOVirtual.DelayedCall(2f, () =>
            {
                playerData.exp += currentMission.expReward;
                playerData.cash += currentMission.cashReward;
                MissionUI.SetActive(false);
                ShowUIComplete();
            });
        }

        public void HandlerMissionFail()
        {
            StartCoroutine( Wait());
            deliveryCountUI.SetActive(false);
            blackBoard.Bag.SetActive(false);
            MissionUI.SetActive(false);
            
            if (currentMission.missionType == MissionType.Delivery)
            {
                
                ShowShipFailUI();
            }
            else
            {
                ShowFightFailUI();
            }
            
        }

        public void HandlerInPickupDelivery()
        {
            deliveryDoneUIs[_currentDeliveryDone].SetActive(true);
            _currentDeliveryDone++;
        }
        
        public void ShowUIComplete()
        {
            blackBoard.Bag.SetActive(false);
            deliveryCountUI.SetActive(false);
            foreach (var child in deliveryDoneUIs)
            {
                child.gameObject.SetActive(false);
            }
            MissionUI.SetActive(false);
            CompleteUI.SetActive(true);
            timeManager.pause = true;
            cashRewardValue.text = currentMission.cashReward.ToString();
            expRewardValue.text = currentMission.expReward.ToString();
        }
        
        public void OffUIComplete()
        {
            timeManager.pause = false;
            CompleteUI.SetActive(false);
        }
        
        private void ShowFightFailUI()
        {
            MissionUI.SetActive(false);
            timeManager.pause = true;
            FailFightUI.SetActive(true);
        }
        
        private void OffFightFailUI()
        {
            timeManager.pause = false;
            FailFightUI.SetActive(false);
        }
        
        private void ShowShipFailUI()
        {
            MissionUI.SetActive(false);
            foreach (var child in deliveryDoneUIs)
            {
                child.gameObject.SetActive(false);
            }
            timeManager.pause = true;
            FailShipUI.SetActive(true);
        }
        
        private void OffShipFailUI()
        {
            FailShipUI.SetActive(false);
            timeManager.pause = false;
        }

        
        public void BtnAdsReward()
        {
            playerData.exp += currentMission.expReward * 2;
            playerData.cash += currentMission.expReward * 2;
            StartCoroutine(ShowAds());
            OffUIComplete();
            StartCoroutine(WaitStartMission(5f));
        }
        
        public void BtnNextComplete()
        {
            playerData.exp += currentMission.expReward;
            playerData.cash += currentMission.cashReward;
            OffUIComplete();
            StartCoroutine(WaitStartMission(5f));
        }
        
        public void BtnNextFail()
        {
            OffFightFailUI();
            StartCoroutine(WaitStartMission(5f));
        }
        
        public void BtnRevive()
        {
            playerData.currentHealth = playerData.maxHealth;
            MissionUI.SetActive(true);
            //TODO tiep tuc
        }
        
        public void BtnShipFail()
        {
            OffShipFailUI();
            Debug.Log("call");
            StartCoroutine(WaitStartMission(5f));
        }
        
        private IEnumerator WaitStartMission(float time)
        {
            yield return new WaitForSeconds(time);
            missionManager.StartMission();
        }

        private IEnumerator ShowAds()
        {
            videoAds1.SetActive(true);
            yield return new WaitForSeconds(3);
            videoAds1.SetActive(false);
            videoAds2.SetActive(true);
            yield return new WaitForSeconds(3);
            videoAds2.SetActive(false);
        }
        
        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(2);
        }
    }
}