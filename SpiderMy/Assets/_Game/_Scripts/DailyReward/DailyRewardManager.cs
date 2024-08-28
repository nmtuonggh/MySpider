using System;
using System.Collections.Generic;
using SFRemastered._Game._Scripts.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered._Game._Scripts.DailyReward
{
    public class DailyRewardManager : MonoBehaviour
    {
        public List<BaseDailyRewardSO> listRewardData;
        public List<ItemUI> listRewardUI;
        [FormerlySerializedAs("dataDailyReward")] public PlayerDataSO playerData;
        public GameObject textClaimed;
        public GameObject btnClaim;
        private DateTime lastClaimedDate;
        
        private const string LastClaimedDateKey = "LastClaimedDate";

        private void OnEnable()
        {
            LoadData();
            SetDataUI();
        }

        private void OnDisable()
        {
            SaveData();
        }

        private void LoadData()
        {
            if (PlayerPrefs.HasKey(LastClaimedDateKey))
            {
                lastClaimedDate = DateTime.Parse(PlayerPrefs.GetString(LastClaimedDateKey));
            }
        }
        
        private void SaveData()
        {
            PlayerPrefs.SetString(LastClaimedDateKey, lastClaimedDate.ToString());
            PlayerPrefs.Save();
        }

        public void SetDataUI()
        {
            for (int i = 0; i < listRewardUI.Count; i++)
            {
                listRewardUI[i].SetData(listRewardData[i].rewardImage, listRewardData[i].amount.ToString());
                listRewardUI[i].SetCheckMark(false);
                listRewardUI[i].SetFocus(false);
            }

            for (int i = 0; i < listRewardUI.Count; i++)
            {
                if (i < playerData.currentDay)
                {
                    listRewardUI[i].SetCheckMark(true);
                }
                else
                {
                    listRewardUI[i].SetCheckMark(false);
                }
            }

            for (int i = 0; i < listRewardUI.Count; i++)
            {
                if (lastClaimedDate.Date != DateTime.Now.Date && i == playerData.currentDay)
                {
                    listRewardUI[i].SetFocus(true);
                    textClaimed.SetActive(false);
                }
            }
            
            if (lastClaimedDate.Date == DateTime.Now.Date)
            {
                btnClaim.SetActive(false);
                textClaimed.SetActive(true);
            }else if (lastClaimedDate.Date != DateTime.Now.Date)
            {
                btnClaim.SetActive(true);
                textClaimed.SetActive(false);
            }
        }

        public void ClaimReward()
        {
            if (lastClaimedDate.Date == DateTime.Now.Date)
            {
                return;
            }

            listRewardData[playerData.currentDay].ClaimReward();
            playerData.currentDay = (playerData.currentDay % 7) + 1;
            lastClaimedDate = DateTime.Now;
            SetDataUI();
        }
    }
}