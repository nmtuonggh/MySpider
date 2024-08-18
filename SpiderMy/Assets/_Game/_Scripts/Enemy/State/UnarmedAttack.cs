using Animancer;
using NodeCanvas.Framework;
using ParadoxNotion;
using SFRemastered._Game._Scripts.Enemy;
using SFRemastered._Game._Scripts.ReferentSO;
using UnityEngine;

namespace SFRemastered
{
    public class UnarmedAttack : BaseAttack
    {
        [SerializeField] private float radius = 0.75f;
        [SerializeField] private ClipTransition[] clip;
        private int randomIndex;

        public override void EnterState()
        {
            base.EnterState();
            randomIndex = Random.Range(0, clip.Length);
            _state = _blackBoard.animancer.Play(clip[randomIndex]);
            _blackBoard.lineRenderer.positionCount = 0;
            _blackBoard.animancer.Animator.applyRootMotion = true;  
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            if (_state.NormalizedTime >= 1f)
            {
                return StateStatus.Success;
            }
            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
            _blackBoard.animancer.Animator.applyRootMotion = false;
        }

        public void OnHit()
        {
            Collider[] hitColliders = Physics.OverlapSphere(_blackBoard.sphereCastCenter.transform.position, radius,
                _blackBoard.hitLayer);
            if (hitColliders.Length > 0)
            {
                foreach (var hitCollider in hitColliders)
                {
                    var target = hitCollider.GetComponent<IHitable>();
                    target.OnStaggerHit(_blackBoard.enemyData.damage);
                }
            }
        }
    }
}