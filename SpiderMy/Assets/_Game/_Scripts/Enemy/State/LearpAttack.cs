using Animancer;
using DG.Tweening;
using SFRemastered._Game._Scripts.Enemy;
using UnityEngine;

namespace SFRemastered
{
    public class LearpAttack : EnemyBaseState
    {
        [SerializeField] private ClipTransition[] clip;
        [SerializeField] private float radius = 0.75f;
        private int randomIndex;
        public override void EnterState()
        {
            base.EnterState();
            randomIndex = Random.Range(0, clip.Length);
            var target = _blackBoard.target.obj.transform.position;
            var direction = target - _blackBoard.characterController.transform.position;
            _blackBoard.lineRenderer.positionCount = 0;
            _blackBoard.animancer.Animator.applyRootMotion = true;
            _state = _blackBoard.animancer.Play(clip[randomIndex]);
        }
        
        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            if(_state.NormalizedTime >= 1f)
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
        
        public void OnHit2()
        {
            Collider[] hitColliders = Physics.OverlapSphere(_blackBoard.transform.position, 2.3f,
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