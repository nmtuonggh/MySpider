using UnityEngine;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/EndPointLaunch")]
    public class EndPointLaunch : StateBase
    {
        [SerializeField] private DiveState _diveState;
        public override void EnterState()
        {
            base.EnterState();
        }

        public override StateStatus UpdateState()
        {
            base.UpdateState();
            
            if (_blackBoard.playerMovement.GetVelocity().y <= -5f)
            {
                _fsm.ChangeState(_diveState);
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