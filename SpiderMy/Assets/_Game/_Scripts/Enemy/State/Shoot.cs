using DG.Tweening;
using UnityEngine;

namespace SFRemastered
{
    public class Shoot : EnemyBaseState
    {
        private bool done = false;
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.lineRenderer.positionCount = 0;
            done = false;
            Bum();
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            _blackBoard.characterController.Move(Vector3.zero);

            if (done)
            {
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }
        
        public override void ExitState()
        {
            base.ExitState();
            done = false;
        }

        private void Bum()
        {
            var lastPos = _blackBoard.target.obj.transform.position;
            var shootPos = _blackBoard.shootPosition.transform.position;
            var target = lastPos + Vector3.up * 0.6f;

            var direction = (target - shootPos).normalized;
            var extendedTarget = target + direction * 5;

            var bullet = _blackBoard.bulletSo.Spawn(shootPos, _blackBoard.transform.rotation, _blackBoard.transform);
            bullet.transform.DOMove(extendedTarget, 0.4f).OnComplete(() =>
            {
                _blackBoard.bulletSo.ReturnToPool(bullet);
                done = true;
            });
        }
    }
}