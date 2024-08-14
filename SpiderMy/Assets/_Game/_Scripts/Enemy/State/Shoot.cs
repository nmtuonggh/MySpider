using DG.Tweening;
using UnityEngine;

namespace SFRemastered
{
    public class Shoot : EnemyBaseState
    {
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.lineRenderer.positionCount = 0;
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

            /*if (elapsedTime >= 1f)
            {
                Bum();
                return StateStatus.Success;
            }*/

            return StateStatus.Running;
        }

        private void Bum()
        {
            var lastPos = _blackBoard.target.obj.transform.position;
            var shootPos = _blackBoard.shootPosition.transform.position;
            var target = lastPos + Vector3.up * shootPos.y * 0.6f;

            var direction = (target - shootPos).normalized;
            var extendedTarget = target + direction * 5;

            var bullet = _blackBoard.bulletSo.Spawn(shootPos, _blackBoard.transform.rotation, _blackBoard.transform);
            bullet.transform.DOMove(extendedTarget, 0.4f).OnComplete(() => _blackBoard.bulletSo.ReturnToPool(bullet));
        }
    }
}