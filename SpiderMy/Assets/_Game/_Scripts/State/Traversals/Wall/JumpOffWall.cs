using Animancer;
using UnityEngine;

namespace SFRemastered.Wall
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/JumpOffWall")]

    public class JumpOffWall : StateBase
    {
        [SerializeField] private FallState _fallState;
        [SerializeField] private float forceValue ;
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.playerMovement.AddForce((-_blackBoard.playerMovement.transform.forward + new Vector3(0,.8f,0))  * forceValue, ForceMode.Impulse);
            _state.Events.OnEnd = () =>
            {
                _fsm.ChangeState(_fallState);
            };
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            if(_blackBoard.playerMovement.GetVelocity().y < 0f)
            {
                _fsm.ChangeState(_fallState);
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