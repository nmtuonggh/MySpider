using System.Collections.Generic;
using Animancer;
using DG.Tweening;
using UnityEngine;

namespace SFRemastered
{
    public class Die : EnemyBaseState
    {
        public List<ClipTransition> dieAnim;
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.disableRB = true;
            _blackBoard.animancer.Animator.applyRootMotion = true;
            var random = Random.Range(0, dieAnim.Count-1);
            _blackBoard.characterController.transform.DOLookAt(_blackBoard.target.obj.transform.position, 0.2f);
            _blackBoard.cantTarget = true;
            _blackBoard.lineRenderer.positionCount = 0;
            _blackBoard.healthBarUI.SetActive(false);
            _state = _blackBoard.animancer.Play(dieAnim[random]);
        }

        public override StateStatus UpdateState()
        {
            base.UpdateState();

            if (_state.NormalizedTime >= 2.5f)
            {
                _blackBoard.die = false;
                _blackBoard.cantTarget = false;
                _blackBoard.disableRB = false;
                _blackBoard.animancer.Animator.applyRootMotion = false;
                _blackBoard.enemyData.ReturnToPool(_blackBoard.enemyData.id, _blackBoard.gameObject);
                return StateStatus.Success;
            }
            
            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
            
        }
    }
}