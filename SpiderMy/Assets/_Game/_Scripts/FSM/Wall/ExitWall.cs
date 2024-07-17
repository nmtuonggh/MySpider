using Animancer;
using UnityEngine;

namespace SFRemastered.Wall
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/ExitWall")]
    public class ExitWall : StateBase
    {
        [SerializeField] private FallState _fallState;
        [SerializeField] private float forceValue;
        [SerializeField] private ClipTransition[] _listClipTransitions;
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.playerMovement.AddForce(_blackBoard.playerMovement.GetVelocity().normalized * forceValue, ForceMode.Impulse);
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            //HandleExitAnim();

            if(_blackBoard.playerMovement.GetVelocity().y < 0)
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
        
        private void HandleExitAnim()
        {
            var angle = Vector3.Angle(_blackBoard.playerMovement.GetVelocity(), _blackBoard.playerMovement.transform.right);
            if (angle is >= 0 and < 60)
            {
                _state = _blackBoard.animancer.Play(_listClipTransitions[0]);
            }
            else if (angle is >= 60 and <= 120)
            {
                _state = _blackBoard.animancer.Play(_listClipTransitions[1]);
            }
            else 
            {
                _state = _blackBoard.animancer.Play(_listClipTransitions[2]);
            }
        }
    }
}