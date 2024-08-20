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
        [SerializeField] private float durationValue;
        [SerializeField] private bool _doneMove;

        public override void EnterState()  
        {
            base.EnterState();
            _blackBoard.playerMovement.SetMovementDirection(Vector3.zero);
            _blackBoard.rigidbody.interpolation = RigidbodyInterpolation.None;
            var distance = Vector3.Distance(_blackBoard.playerMovement.transform.position,
                _blackBoard.raycastCheckWall.zipPoint);
            float moveDuration = distance / durationValue;
            _blackBoard.playerMovement.transform.DOMove(_blackBoard.raycastCheckWall.zipPoint + new Vector3(0, 0.2f, 0),
                    moveDuration).OnComplete(() => { _doneMove = true; });
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            _blackBoard.playerMovement.SetMovementDirection(Vector3.zero);

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
            _blackBoard.lr.positionCount = 0;
            _doneMove = false;
            _fsm.transform.DORotate(
                Quaternion.LookRotation(_fsm.transform.forward.projectedOnPlane(Vector3.up), Vector3.up).eulerAngles,
                0.2f);
            _blackBoard.rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        }
    }
}