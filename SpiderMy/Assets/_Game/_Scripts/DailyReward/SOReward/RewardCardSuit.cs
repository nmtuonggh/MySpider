using UnityEngine;

namespace SFRemastered._Game._Scripts.DailyReward
{
    [CreateAssetMenu(menuName = "ScriptableObjects/DailyReward/CardSuit")]
    public class RewardCardSuit : BaseDailyRewardSO
    {
        public override void ClaimReward()
        {
            playerData.AddCash(amount);
        }
    }
}