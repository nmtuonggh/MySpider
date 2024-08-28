using UnityEngine;

namespace SFRemastered._Game._Scripts.DailyReward
{
    [CreateAssetMenu(menuName = "ScriptableObjects/DailyReward/Exp")]
    public class RewardExp : BaseDailyRewardSO
    {
        public override void ClaimReward()
        {
            playerData.AddExp(amount);
        }
    }
}