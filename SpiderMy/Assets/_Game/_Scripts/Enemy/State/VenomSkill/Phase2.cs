using DG.Tweening;
using SFRemastered._Game._Scripts.Enemy;
using UnityEngine;

namespace SFRemastered.VenomSkill
{
    public class Phase2 : EnemyBaseState
    {
        public override void EnterState()
        {
            base.EnterState();
            var target = _blackBoard.target.obj.transform.position;
            var targetDir = target - transform.position;
            targetDir.y = 0;
            _blackBoard.animancer.Animator.applyRootMotion = true;
            _blackBoard.characterController.transform.rotation = Quaternion.LookRotation(targetDir);
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
        
        public void OnHitPhase1()
        {
            Collider[] hitColliders = Physics.OverlapSphere(_blackBoard.sphereCastCenter.transform.position, 1.5f,
                _blackBoard.hitLayer);
            if (hitColliders.Length > 0)
            {
                foreach (var hitCollider in hitColliders)
                {
                    var target = hitCollider.GetComponent<PlayerController>();
                    var direction = transform.position - target.transform.position;
                    var rotation = new Vector3(direction.x, target.transform.position.y, direction.z);
                    target.transform.rotation = Quaternion.LookRotation(rotation);
                    if (!hitCollider.GetComponent<BlackBoard>().invincible)
                    {
                        target.OnVenomPhase1Hit(0);
                    }
                    
                }
            }
        }
        
        public void OnHitPhase2()
        {
            Collider[] hitColliders = Physics.OverlapSphere(_blackBoard.sphereCastCenter.transform.position, 1.5f,
                _blackBoard.hitLayer);
            if (hitColliders.Length > 0)
            {
                foreach (var hitCollider in hitColliders)
                {
                    var target = hitCollider.GetComponent<PlayerController>();
                    var direction = transform.position - target.transform.position;
                    var rotation = new Vector3(direction.x, target.transform.position.y, direction.z);
                    target.transform.rotation = Quaternion.LookRotation(rotation);
                    if (!hitCollider.GetComponent<BlackBoard>().invincible)
                    {
                        target.OnVenomPhase2Hit(_blackBoard.enemyData.damage/2);
                    }
                    
                }
            }
        }
        
        public void OnMiniHit()
        {
            Collider[] hitColliders = Physics.OverlapSphere(_blackBoard.sphereCastCenter.transform.position, 1.5f,
                _blackBoard.hitLayer);
            if (hitColliders.Length > 0)
            {
                foreach (var hitCollider in hitColliders)
                {
                    var target = hitCollider.GetComponent<PlayerController>();
                    if (!hitCollider.GetComponent<BlackBoard>().invincible)
                    {
                        target.OnVenomMiniHit(_blackBoard.enemyData.damage/3);
                    }
                    
                }
            }
        }
        
        public void OnFinalHit()
        {
            Collider[] hitColliders = Physics.OverlapSphere(_blackBoard.sphereCastCenter.transform.position, 1.5f,
                _blackBoard.hitLayer);
            if (hitColliders.Length > 0)
            {
                foreach (var hitCollider in hitColliders)
                {
                    var target = hitCollider.GetComponent<PlayerController>();
                    var direction = transform.position - target.transform.position;
                    var rotation = new Vector3(direction.x, target.transform.position.y, direction.z);
                    target.transform.rotation = Quaternion.LookRotation(rotation);
                    
                    if (!hitCollider.GetComponent<BlackBoard>().invincible)
                    {
                        target.OnVenomFinalHit(_blackBoard.enemyData.damage/2);
                    }
                }
            }
        }
    }
}