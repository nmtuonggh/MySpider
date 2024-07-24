using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/Zip")]
    public class ZipState : StateBase
    {
        [SerializeField] private ZipEnd _zipEnd;
        [SerializeField] private ZipPointJump _zipPointJump;

        [SerializeField] private ClipTransition _startZip;
        [SerializeField] private ClipTransition _zipping;
        [SerializeField] private ClipTransition _endZip;
        
        [SerializeField] private float durationValue;
        [SerializeField] private bool _doneMove;

        public override void EnterState()  
        {
            base.EnterState();
            _state = _blackBoard.animancer.Play(_startZip);
            _doneMove = false;
            _blackBoard.playerMovement.SetMovementMode(MovementMode.None);
            _blackBoard.rigidbody.useGravity = false;
            _blackBoard.rigidbody.isKinematic = false;

            var distance = Vector3.Distance(_blackBoard.playerMovement.transform.position,
                _blackBoard.findZipPoint.zipPoint + new Vector3(0, 0.2f, 0));
            float moveDuration = distance / durationValue;
            DrawWeb();
            _blackBoard.playerMovement.transform.DOLookAt(_blackBoard.findZipPoint.zipPoint, 0.1f, AxisConstraint.Y);
            _blackBoard.playerMovement.transform.DOMove(_blackBoard.findZipPoint.zipPoint + new Vector3(0, 0.2f, 0),
                    moveDuration).OnComplete(() => { _doneMove = true; });
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            _state = _blackBoard.animancer.Play(_zipping);

            if (_doneMove)
            {
                if (_blackBoard.zip == false)
                {
                    _fsm.ChangeState(_zipEnd);
                }else
                {
                    _fsm.ChangeState(_zipPointJump);
                }

                return StateStatus.Success;
            }
            
            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
            DOTween.Kill(_blackBoard.playerMovement.gameObject);
            _blackBoard.lr.positionCount = 0;
            _doneMove = false;
            _fsm.transform.DORotate(
                Quaternion.LookRotation(_fsm.transform.forward.projectedOnPlane(Vector3.up), Vector3.up).eulerAngles,
                0.2f);
            _blackBoard.playerMovement.SetMovementMode(MovementMode.Walking);
            _blackBoard.rigidbody.useGravity = false;
            _blackBoard.rigidbody.isKinematic = true;
            _blackBoard.rigidbody.constraints = RigidbodyConstraints.None;
        }
        
        private void DrawWeb()
        {
            /*DrawWebLeft();
            DrawWebRight();*/
            _blackBoard.lr.positionCount = 3;
            _blackBoard.lr.SetPosition(1, _blackBoard.findZipPoint.zipPoint);
            _blackBoard.lr.SetPosition(2, _blackBoard.startZipRight.position);
            _blackBoard.lr.SetPosition(0, _blackBoard.startZipLeft.position);
        }

        private void DrawWebLeft()
        {
            _blackBoard.lr.SetPosition(1, _blackBoard.findZipPoint.zipPoint);
            _blackBoard.lr.SetPosition(0, _blackBoard.startZipLeft.position);
        }
        private void DrawWebRight()
        {
            _blackBoard.lr.SetPosition(1, _blackBoard.findZipPoint.zipPoint);
            _blackBoard.lr.SetPosition(0, _blackBoard.startZipRight.position);
        }
    }
}