﻿using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/StartZip")]
    public class StartZip : ZipBase
    {
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.currentZipPoint = _blackBoard.raycastCheckWall.zipPoint;
            _blackBoard.rigidbody.interpolation = RigidbodyInterpolation.None;
            _blackBoard.playerMovement.transform.DOLookAt(_blackBoard.currentZipPoint, 0.2f, AxisConstraint.Y).OnComplete(DrawWeb);
            
            _blackBoard.playerMovement.SetMovementMode(MovementMode.None);
            _blackBoard.rigidbody.useGravity = false;
            _blackBoard.rigidbody.isKinematic = false;
            _blackBoard.rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        }
        
        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            _blackBoard.playerMovement.SetMovementDirection(Vector3.zero);
            if (_state.NormalizedTime >=1)
            {
                _fsm.ChangeState(_blackBoard.stateReference.ZipState);
                return StateStatus.Success;
            }
            
            return StateStatus.Running;
        }
        
        public override void ExitState()
        {
            base.ExitState();
            _blackBoard.playerMovement.SetMovementMode(MovementMode.Walking);
            _blackBoard.rigidbody.useGravity = false;
            _blackBoard.rigidbody.isKinematic = true;
            _blackBoard.rigidbody.constraints = RigidbodyConstraints.None;
        }
        
        private void DrawWeb()
        {
            _blackBoard.lr.positionCount = 3;
            _blackBoard.lr.SetPosition(1, _blackBoard.currentZipPoint);
            _blackBoard.lr.SetPosition(2, _blackBoard.startZipRight.position);
            _blackBoard.lr.SetPosition(0, _blackBoard.startZipLeft.position);
        }
    }
}