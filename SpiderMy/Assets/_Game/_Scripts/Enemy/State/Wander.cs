using ParadoxNotion;
using UnityEngine;

namespace SFRemastered
{
    public class Wander : EnemyBaseState
    {
        [SerializeField] private float wanderRadius;
        
        private Transform wanderPos;

        public override void EnterState()
        {
            base.EnterState();
            //wanderPos = random a position in wander radius
            wanderPos = new GameObject().transform;
            wanderPos.position =Random.insideUnitSphere * wanderRadius;
        }

        public override StateStatus UpdateState() 
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            if (_blackBoard.wanderPositionIndex == 0)
            {
                _blackBoard.characterController.Move(wanderPos.position * 1 * Time.deltaTime);
                if (Vector3.Distance(_blackBoard.characterController.transform.position, wanderPos.position) < 0.05f)
                {
                    _blackBoard.wanderPositionIndex = 1;
                    return StateStatus.Success;
                }
            }
            else
            {
                _blackBoard.characterController.Move(_blackBoard.startWanderPosition * 1 * Time.deltaTime);
                if (Vector3.Distance(_blackBoard.characterController.transform.position, _blackBoard.startWanderPosition) < 0.05f)
                {
                    _blackBoard.wanderPositionIndex = 0;
                    return StateStatus.Success;
                }
            }

            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}