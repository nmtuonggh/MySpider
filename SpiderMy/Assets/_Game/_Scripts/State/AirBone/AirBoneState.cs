using SFRemastered.Wall;
using UnityEngine;

namespace SFRemastered
{
    public class AirBoneState : StateBase
    {
        [SerializeField] protected WallRun _wallRun;
        public override void EnterState()
        {
            base.EnterState();
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public override StateStatus UpdateState()
        {
            base.UpdateState();

            if (_blackBoard.playerMovement.foudWall)
            {
                _fsm.ChangeState(_wallRun);
                return StateStatus.Success;
            }
            
            return StateStatus.Running;
        }
    }
}