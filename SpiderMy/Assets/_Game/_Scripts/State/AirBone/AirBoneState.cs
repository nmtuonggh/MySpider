using SFRemastered.Combat.ZipAttack;
using SFRemastered.Wall;
using UnityEngine;

namespace SFRemastered
{
    public class AirBoneState : StateBase
    {
        [SerializeField] protected WallRun _wallRun;
        [SerializeField] protected ZipState _zipState;
        [SerializeField] protected StartZipAttack _startZipAttack;
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

            if (_blackBoard.foundWall)
            {
                _fsm.ChangeState(_wallRun);
                return StateStatus.Success;
            }
            
            if (_blackBoard.zip && _blackBoard.findZipPoint.focusZipPointPrefab.gameObject.activeSelf)
            {
                _fsm.ChangeState(_zipState);
                return StateStatus.Success;
            }

            if (_blackBoard._detectedEnemy && _blackBoard.attack)
            {
                _fsm.ChangeState(_startZipAttack);
                return StateStatus.Success;
            }
            
            return StateStatus.Running;
        }
    }
}