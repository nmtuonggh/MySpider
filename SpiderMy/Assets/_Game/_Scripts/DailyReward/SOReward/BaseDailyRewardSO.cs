using SFRemastered._Game._Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace SFRemastered._Game._Scripts.DailyReward
{
    public class BaseDailyRewardSO : ScriptableObject
    {
        public Sprite rewardImage;
        public PlayerDataSO playerData;
        public float amount;
        public virtual void ClaimReward()
        {
        }
    }
}