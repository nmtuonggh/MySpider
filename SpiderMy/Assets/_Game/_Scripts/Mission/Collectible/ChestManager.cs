using System;
using SFRemastered._Game._Scripts.Data;
using TMPro;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission.Collectible
{
    public class ChestManager : MonoBehaviour
    {
        private Chest currentChest;
        public Transform chestPosition;
        public GameObject chestPrefab;
        public GameObject btnClaim;

        public PlayerDataSO playerData;
        [Header("UI Elements")] public GameObject UICollect;
        public TextMeshProUGUI cash;
        public TextMeshProUGUI exp;

        private void OnEnable()
        {
            foreach (Transform pos in chestPosition)
            {
                Instantiate(chestPrefab, pos.position, pos.rotation, pos);
            }
        }

        public void SetCurrentChest(Chest chest)
        {
            currentChest = chest;
        }

        public void OpenCurrentChest()
        {
            if (currentChest != null)
            {
                currentChest.OpenChest();
                btnClaim.SetActive(false);
                PopUpReward();
            }
        }

        private void PopUpReward()
        {
            UICollect.SetActive(true);
            if (playerData.level < 16)
            {
                playerData.GetCurrentCoefficient();
                playerData.GetXpToNextLevel();
                exp.text = playerData.xpToNextLevel * 0.1f + "";
                cash.text = playerData.xpToNextLevel * 0.1f + 50f + "";
            }
            else
            {
                playerData.GetCurrentCoefficient();
                playerData.GetXpToNextLevel();
                exp.text = playerData.xpToNextLevel * 0.1f + "";
                cash.text = playerData.xpToNextLevel * 0.1f + 100f + "";
            }
        }
    }
}