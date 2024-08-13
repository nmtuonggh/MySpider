using UnityEngine;

namespace SFRemastered
{
    public class Shoot : EnemyBaseState
    {
        public override void EnterState()
        {
            base.EnterState();
            Bum();
        }
        
        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if(baseStatus != StateStatus.Running)
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
            var rotation = Quaternion.LookRotation(_blackBoard.target.obj.transform.position - _blackBoard.shootPosition.transform.position);
            _blackBoard.bulletSo.Spawn(_blackBoard.shootPosition.transform.position, rotation, _blackBoard.gameObject.transform);
        }
        
    }
}