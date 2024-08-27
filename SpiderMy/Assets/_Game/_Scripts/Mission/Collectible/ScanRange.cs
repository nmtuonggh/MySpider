using System;
using System.Collections.Generic;
using _Game.Scripts.Event;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission.Collectible
{
    public class ScanRange : MonoBehaviour
    {
        [SerializeField] private List<Collider> _chestList;
        public GameEvent haveChestInRange;
        public GameEvent noChestInRange;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Chest"))
            {
                OnNoti();
                _chestList.Add(other);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Chest"))
            {
                OffNoti();
                _chestList.Remove(other);
            }
        }

        public void OnNoti()
        {
            if(_chestList.Count <= 0)
            {
                haveChestInRange.Raise();
            }
        }
        
        public void OffNoti()
        {
            if(_chestList.Count == 1)
            {
                noChestInRange.Raise();
            }
        }
    }
}