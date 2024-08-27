using System;
using SFRemastered._Game._Scripts;
using SFRemastered._Game._Scripts.Mission.Collectible;
using UnityEngine;

namespace SFRemastered
{
    public class ChestCollectRange : MonoBehaviour
    {
        public GameObject btnClaim;
        public ChestManager chestManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Chest"))
            {
                var chest = other.GetComponent<Chest>();
                if (chest != null)
                {
                    btnClaim.SetActive(true);
                    chestManager.SetCurrentChest(chest);
                }
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Chest"))
            {
                btnClaim.SetActive(false);
            }
        }
    }
}