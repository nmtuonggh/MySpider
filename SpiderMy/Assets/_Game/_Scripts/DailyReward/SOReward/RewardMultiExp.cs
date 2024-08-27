using UnityEngine;

namespace SFRemastered._Game._Scripts.DailyReward
{
    [CreateAssetMenu(menuName = "ScriptableObjects/DailyReward/MultiExp")]
    public class RewardMultiExp : BaseDailyRewardSO
    {
        public float times;
        public float multiplier;
        
        public override void ClaimReward()
        {
            playerData.multiplierXp = multiplier;
            playerData.timeMulti = times * amount;
        }
    }
}