using System;
using DG.Tweening;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission.UIPopup
{
    public class UIMissionComplete : MonoBehaviour
    {
        public GameObject btnAds;
        public GameObject btnCollect;

        private void OnEnable()
        {
            btnAds.SetActive(true);
            DOVirtual.DelayedCall(3f, () =>
            {
                btnCollect.SetActive(true);
            });
        }

        private void OnDisable()
        {
            btnAds.SetActive(false);
            btnCollect.SetActive(false);
        }
    }
}