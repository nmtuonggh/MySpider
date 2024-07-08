using System;
using UnityEngine;

namespace SFRemastered._Game._Scripts.FSM.Swing
{
    public class SwingPoint : MonoBehaviour
    {
        public Vector3 point;
        public Transform playerPoint;

        private void Update()
        {
            point = playerPoint.position + new Vector3(0, 15f, 10f);
        }
    }
}