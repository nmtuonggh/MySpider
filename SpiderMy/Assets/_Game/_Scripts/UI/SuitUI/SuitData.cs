using System;
using UnityEngine;

namespace SFRemastered
{
    public class SuitData : MonoBehaviour
    {
        public int suitID;
        
        public Avatar avatar;

        public Transform ZipPointLeft;
        public Transform ZipPointRight;
        public Transform HandSwing;
        public Transform RopeShotPosition;
        public Transform ZipAttackPoint;
        public GameObject backPack;


        private void OnValidate()
        {
            ZipPointLeft = FindDeepChild(transform, "ZipPointLeft");
            ZipPointRight = FindDeepChild(transform, "ZipPointRight");
            HandSwing = FindDeepChild(transform, "HandSwing");
            RopeShotPosition = FindDeepChild(transform, "RopeShotPosition");
            ZipAttackPoint = FindDeepChild(transform, "ZipAttackPoint");
            backPack = FindDeepChild(transform, "BackPack").gameObject;
        }

        private Transform FindDeepChild(Transform parent, string name)
        {
            foreach (Transform child in parent)
            {
                if (child.name == name)
                    return child;
                var result = FindDeepChild(child, name);
                if (result != null)
                    return result;
            }

            return null;
        }


        public void ApplySkin(BlackBoard blackBoard)
        {
            blackBoard.playerSwingPos = HandSwing;
            blackBoard.startrope = RopeShotPosition;
            blackBoard.startZipLeft = ZipPointLeft;
            blackBoard.startZipRight = ZipPointRight;
            blackBoard._zipAttackHandPositon = ZipAttackPoint;
            blackBoard.Bag = backPack;
        }
    }
}