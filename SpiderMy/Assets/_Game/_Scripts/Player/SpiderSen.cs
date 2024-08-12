using System;
using _Game.Scripts.Event;
using UnityEngine;

namespace SFRemastered
{
    public class SpiderSen : MonoBehaviour
    {
        [SerializeField] private GameObject spiderSen;
        private int spiderSenCount = 0;
        
        public GameEventListener onEnableSpiderSen;
        public GameEventListener onDisableSpiderSen;

        private void OnEnable()
        {
            onEnableSpiderSen.OnEnable();
            onDisableSpiderSen.OnEnable();
        }

        private void OnDisable()
        {
            onDisableSpiderSen.OnDisable();
            onEnableSpiderSen.OnDisable();
        }
        
        public void EnableSpiderSen()
        {
            spiderSenCount++;
            if (spiderSenCount == 1)
            {
                spiderSen.SetActive(true);
            }
        }
        
        public void DisableSpiderSen()
        {
            spiderSenCount--;
            if (spiderSenCount == 0)
            {
                spiderSen.SetActive(false);
            }
        }
    }
}