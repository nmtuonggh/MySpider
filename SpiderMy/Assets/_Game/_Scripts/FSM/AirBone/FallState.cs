using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/Fall")]
    public class FallState : FallDownBaseState
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
            
            if (_blackBoard.playerMovement.GetVelocity().y < 0 && _blackBoard.playerMovement.GetVelocity().magnitude > 20f)
            {
                _fsm.ChangeState(_diveState);
                return StateStatus.Success;
            }
            
            return StateStatus.Running;
        }
    }
}
