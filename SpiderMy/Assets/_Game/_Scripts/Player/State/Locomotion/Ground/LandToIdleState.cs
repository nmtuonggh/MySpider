using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/LandToIdle")]
    public class LandToIdleState : GroundState
    {
        [SerializeField] private IdleState _idleState;
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.characterVisual.transform.rotation = Quaternion.LookRotation(_blackBoard.playerMovement.transform.forward, Vector3.up);

            _state.Events.OnEnd = () =>
            {
                _fsm.ChangeState(_idleState);
            };
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            return StateStatus.Running;
        }
    }
}
