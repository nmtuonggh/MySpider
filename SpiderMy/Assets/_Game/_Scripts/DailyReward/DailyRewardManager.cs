using System;
using System.Collections.Generic;
using SFRemastered._Game._Scripts.Data;
using UnityEngine;

namespace SFRemastered._Game._Scripts.DailyReward
{
    public class DailyRewardManager : MonoBehaviour
    {
        public List<BaseDailyRewardSO> listRewardData;
        public List<ItemUI> listRewardUI;
        public PlayerDataSO dataDailyReward;
        private int currentIndex;

        private void OnEnable()
        {
            SetDataUI();
            currentIndex = dataDailyReward.currentDay;
        }
        
        public void SetDataUI()
        {
            for (int i = 0; i < listRewardUI.Count-1; i++)
            {
                listRewardUI[i].SetData(listRewardData[i].rewardImage, listRewardData[i].amount.ToString());
                listRewardUI[i].SetCheckMark(false);
                listRewardUI[i].SetFocus(false);
            }

            for (int i = 0; i < listRewardUI.Count-1; i++)
            {
                if (i < dataDailyReward.currentDay-1)
                {
                    listRewardUI[i].SetCheckMark(true);
                }
                else
                {
                    listRewardUI[i].SetCheckMark(false);
                }
            }

            if (dataDailyReward.currentDay > 0 && dataDailyReward.currentDay <= listRewardUI.Count-1)
            {
                listRewardUI[dataDailyReward.currentDay-1].SetFocus(true);
            }
        }
        
        public void ClaimReward()
        {
            string savedDate = PlayerPrefs.GetString("LastClaimedDate", DateTime.Now.ToString());
            DateTime lastClaimedDate = DateTime.Parse(savedDate);
            
            if (DateTime.Now.Date > lastClaimedDate.Date)
            {
                listRewardData[currentIndex].ClaimReward();
                dataDailyReward.currentDay = (dataDailyReward.currentDay % 7) + 1;
                
                PlayerPrefs.SetString("LastClaimedDate", DateTime.Now.ToString());

                // Update UI
                SetDataUI();
            }
        }
        
    }
}