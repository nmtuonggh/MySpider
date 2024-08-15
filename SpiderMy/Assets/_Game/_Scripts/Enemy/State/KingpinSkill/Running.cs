using System;
using System.Numerics;
using SFRemastered._Game._Scripts.Enemy;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace SFRemastered.KingpinSkill
{
    public class Running : EnemyBaseState
    {
        [SerializeField] private float speed;
        [SerializeField] private float time;
        private Vector3 targetPos;
        private Vector3 direction;
        
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
        }

        void OverlapSphere()
        {
            Collider[] hitColliders = Physics.OverlapSphere(_blackBoard.characterController.transform.position, 1.8f, _blackBoard.hitLayer);
            int i = 0;
            while (i < hitColliders.Length)
            {
                var target = hitColliders[i].GetComponent<IHitable>();
                target.OnKnockBackHit(_blackBoard.enemyData.damage);
                i++;
            }
        }
    }
}