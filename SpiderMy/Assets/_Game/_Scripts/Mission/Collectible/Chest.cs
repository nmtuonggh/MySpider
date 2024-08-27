using System;
using Animancer;
using DG.Tweening;
using SFRemastered._Game._Scripts.Data;
using TMPro;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission.Collectible
{
    public class Chest : MonoBehaviour
    {
        public Animator animator;
        public AnimancerComponent animancer;
        public bool opened;
        private float time;
        public void OpenChest()
        {
            animator.SetTrigger("Open");
            DOVirtual.DelayedCall(5f, () =>
            {
                this.gameObject.SetActive(false);
                opened = true;
                time = 0;
                animator.SetTrigger("Close");
            });
            
        }

        private void Update()
        {
            if (opened)
            {
                time += Time.deltaTime;
                if (time > 300)
                {
                    this.gameObject.SetActive(true);
                    time = 0;
                    opened = false;
                }
            }
        }
    }
}