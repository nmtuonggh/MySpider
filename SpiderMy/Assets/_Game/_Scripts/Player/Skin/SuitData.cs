using System;
using UnityEngine;

namespace SFRemastered
{
    public class SuitData : MonoBehaviour
    {
        public GameObject skinPrefab;
        
        public float health;
        public float damage;
        public float speed;
        
        public Transform zipPointLeft;
        public Transform zipPointRight;
        public Transform handSwing;
        public Transform ropeShotPos;
        public Transform zipAttackPoint;

        public void ApplySkin(BlackBoard blackBoard)
        {
            blackBoard.playerSwingPos = handSwing;
            blackBoard.startrope = ropeShotPos;
            blackBoard.startZipLeft = zipPointLeft;
            blackBoard.startZipRight = zipPointRight;
            blackBoard._zipAttackHandPositon = zipAttackPoint;
        }
    }
}