using UnityEngine;

namespace SFRemastered._Game._Scripts.DailyReward
{
    [CreateAssetMenu(menuName = "ScriptableObjects/DailyReward/RewardCash")]
    public class RewardCash : BaseDailyRewardSO
    {
        public override void ClaimReward()
        {
            Debug.Log("Claim cash");
            playerData.cash += amount;
        }
    }
}