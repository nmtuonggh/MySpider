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
        #region Variables

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
        [SerializeField] private GameObject FailFightByDeadUI;
        [SerializeField] private GameObject FailShipUI;
        [SerializeField] private TextMeshProUGUI cashRewardValue;
        [SerializeField] private TextMeshProUGUI expRewardValue;
        [SerializeField] private Slider expSlider;
        [SerializeField] private TextMeshProUGUI currentLV;
        [SerializeField] private GameObject videoAds1;
        [SerializeField] private GameObject videoAds2;
        [SerializeField] private GameObject warningPopup;
        
        [Header("==========Mission Ship==========")]
        public GameObject deliveryCountUI;
        public List<GameObject> deliveryDoneUIs;
        private int _currentDeliveryDone;

        [Header("==========Mission Event ==========")]
        public GameEvent dontWantRevive;
        
        [Header("==========Mission Event Listener==========")]
        public GameEventListener onMissionStart;
        public GameEventListener onMissionUpdate;
        public GameEventListener onMissionComplete;
        public GameEventListener onMissionFail;
        public GameEventListener inMissionRange;
        public GameEventListener inPickupDelivery;
        public GameEventListener inWarningRange;
        public GameEventListener outWarningRange;
        public GameEventListener onMissionFailByDie;

        #endregion

        #region Register Event

        private void OnEnable()
        {
            onMissionStart.OnEnable();
            onMissionUpdate.OnEnable();
            onMissionComplete.OnEnable();
            onMissionFail.OnEnable();
            inMissionRange.OnEnable();
            inPickupDelivery.OnEnable();
            inWarningRange.OnEnable();
            outWarningRange.OnEnable();
            //onMissionFailByDie.OnEnable();
        }

        private void OnDisable()
        {
            onMissionStart.OnDisable();
            onMissionUpdate.OnDisable();
            onMissionComplete.OnDisable();
            onMissionFail.OnDisable();
            inMissionRange.OnDisable();
            inPickupDelivery.OnDisable();
            inWarningRange.OnDisable();
            outWarningRange.OnDisable();
        }

        #endregion

        #region Handler State Mission

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
            warningPopup.SetActive(false);
            switch (currentMission.missionType)
            {
                case MissionType.Fighting:
                    step2Mission.text = "Kill all enemies";
                    break;
                case MissionType.Delivery:
                    step2Item.SetActive(true);
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
                warningPopup.SetActive(false);
                ShowUIComplete();
            });
        }

        public void HandlerMissionFail()
        {
            deliveryCountUI.SetActive(false);
            blackBoard.Bag.SetActive(false);
            MissionUI.SetActive(false);
            warningPopup.SetActive(false);
            
            if (currentMission.missionType == MissionType.Delivery)
            {
                
                ShowShipFailUI();
            }
            else
            {
                ShowFightFailUI();
            }
            
        }
        
        public void HandlerMissionFailByDie()
        {
            deliveryCountUI.SetActive(false);
            blackBoard.Bag.SetActive(false);
            MissionUI.SetActive(false);
            warningPopup.SetActive(false);
            ShowDeadFailUI();
        }

        public void HandlerInPickupDelivery()
        {
            deliveryDoneUIs[_currentDeliveryDone].SetActive(true);
            _currentDeliveryDone++;
        }
        
        public void HandlerOutWarningRange()
        {
            warningPopup.SetActive(true);
        }
        
        public void HandlerInWarningRange()
        {
            step2Item.SetActive(true);
            warningPopup.SetActive(false);
        }

        #endregion

        #region UI Handler

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
        
        private void ShowDeadFailUI()
        {
            timeManager.focus = true;
            DOVirtual.DelayedCall(2f, () =>
            {
                timeManager.focus = false;
                MissionUI.SetActive(false);
                timeManager.pause = true;
                FailFightByDeadUI.SetActive(true);
            });
        }
        
        private void OffDeadFailUI()
        {
            timeManager.pause = false;
            FailFightByDeadUI.SetActive(false);
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

        #endregion

        #region Btn Event

        public void BtnAdsReward()
        {
            playerData.exp += currentMission.expReward * 2;
            playerData.cash += currentMission.expReward * 2;
            Ads();
            OffUIComplete();
            DOVirtual.DelayedCall(10f, () =>
            {
                missionManager.StartMission();;
            });
        }
        
        public void BtnNextComplete()
        {
            playerData.exp += currentMission.expReward;
            playerData.cash += currentMission.cashReward;
            OffUIComplete();
            DOVirtual.DelayedCall(10f, () =>
            {
                missionManager.StartMission();;
            });
        }
        
        public void BtnNextFail()
        {
            OffFightFailUI();
            OffDeadFailUI();
            DOVirtual.DelayedCall(10f, () =>
            {
                missionManager.StartMission();;
            });
        }
        
        public void BtnRevive()
        {
            playerData.currentHealth = playerData.maxHealth;
            MissionUI.SetActive(true);
            Ads();
            OffDeadFailUI();
            //TODO tiep tuc
        }
        
        public void BtnNoRevive()
        {
            OffFightFailUI();
            OffDeadFailUI();
            dontWantRevive.Raise();
            DOVirtual.DelayedCall(10f, () =>
            {
                missionManager.StartMission();;
            });
        }
        
        public void BtnShipFail()
        {
            OffShipFailUI();
            Debug.Log("call");
            DOVirtual.DelayedCall(10f, () =>
            {
                missionManager.StartMission();;
            });
        }

        #endregion
        
        private void Ads()
        {
            videoAds1.SetActive(true);
            DOVirtual.DelayedCall(3f, () =>
            {
                videoAds1.SetActive(false);
                videoAds2.SetActive(true);
                DOVirtual.DelayedCall(3f, () =>
                {
                    videoAds2.SetActive(false);
                });
            });
        }
        
    }
}