using System;
using UnityEngine;

namespace SFRemastered._Game._Scripts.CastCheck.Raycast
{
    public class CheckWallState : MonoBehaviour
    {
        [SerializeField] private BlackBoard _blackBoard;
        [SerializeField] private LayerMask wallLayer;
        [SerializeField] private Transform raycastPos;
        [SerializeField] private float rayCastDistance;
        public RaycastHit hit;
        private void Update()
        {
            RaycastCheck();
        }

        private void RaycastCheck()
        {
            var forward = transform.forward;
            _blackBoard.foundWall = Physics.Raycast(raycastPos.position, forward, out hit, rayCastDistance, wallLayer);
            Debug.DrawLine(raycastPos.position, raycastPos.position + forward * rayCastDistance, Color.red);
        }
    }
}