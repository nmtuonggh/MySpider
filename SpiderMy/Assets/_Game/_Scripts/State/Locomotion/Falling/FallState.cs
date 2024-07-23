using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/Fall")]
    public class FallState : FallingState
    {
        [SerializeField] private DiveState _diveState;
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
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            if (_blackBoard.swing)
            {
                _fsm.ChangeState(_swingState);
                return StateStatus.Success;
            }
            
            if (_blackBoard.playerMovement.GetVelocity().y < 0 && _blackBoard.playerMovement.GetVelocity().magnitude > 35f)
            {
                _fsm.ChangeState(_diveState);
                return StateStatus.Success;
            }
            
            return StateStatus.Running;
        }
    }
}
