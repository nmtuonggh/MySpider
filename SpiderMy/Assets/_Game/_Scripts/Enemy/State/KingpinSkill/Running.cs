using System;
using System.Numerics;
using _Game.Scripts.Event;
using SFRemastered._Game._Scripts.Enemy;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace SFRemastered.KingpinSkill
{
    public class Running : EnemyBaseState
    {
        [SerializeField] private float speed;
        [SerializeField] private float time;
        [SerializeField] private float radius = 1.8f;
        private Vector3 targetPos;
        private Vector3 direction;
        
        public GameEvent onDisableSpiderSense;
        
        public override void EnterState()
        { 
            base.EnterState();
            targetPos = _blackBoard.target.obj.transform.position;
            direction = _blackBoard.target.obj.transform.position - _blackBoard.characterController.transform.position;
        }
    
        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if(baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            _blackBoard.characterController.Move(_blackBoard.characterController.transform.forward * speed * Time.deltaTime);

            OverlapSphere();

            if (elapsedTime >= time)
            {
                return StateStatus.Success;
            }
            return StateStatus.Running;
        }
    
        public override void ExitState()
        {
            base.ExitState();
            radius = 1.8f;
            if (_blackBoard.attacking)
            {
                _blackBoard.warningAttack.SetActive(false);
                _blackBoard.attacking = false;
                onDisableSpiderSense.Raise();
            }
        }

        void OverlapSphere()
        {
            Collider[] hitColliders = Physics.OverlapSphere(_blackBoard.characterController.transform.position, radius, _blackBoard.hitLayer);
            if (hitColliders.Length > 0)
            {
                var target = hitColliders[0].GetComponent<IHitable>();
                if (!hitColliders[0].GetComponent<BlackBoard>().invincible)
                {
                    target.OnKnockBackHit(_blackBoard.enemyData.damage);
                }
                
                radius = 0;
                if (_blackBoard.attacking)
                {
                    _blackBoard.warningAttack.SetActive(false);
                    _blackBoard.attacking = false;
                    onDisableSpiderSense.Raise();
                }
            }
        }
    }
}