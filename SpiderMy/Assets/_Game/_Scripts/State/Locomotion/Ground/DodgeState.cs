using Animancer;
using SFRemastered._Game._Scripts.State.Combat.IdleCombat.SFRemastered.Combat;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered._Game._Scripts.State.Locomotion.Ground
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/DodgeState")]
    public class DodgeState : StateBase
    {
        [SerializeField] private LowIdleCombat lowIdleCombat;
        [SerializeField] private SprintState _sprintState;

        [SerializeField] private ClipTransition _dodgeAnimLeft;
        [SerializeField] private ClipTransition _dodgeAnimRight;
        [SerializeField] private ClipTransition _dodgeAnimBack;
        private bool _done = false;

        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.playerMovement.useRootMotion = true;
            _blackBoard.playerMovement.rootmotionSpeedMult = 1;
            _state = _blackBoard.animancer.Play(HandelDodge());
            _state.Time = 0;
            _state.Events.OnEnd = () => _done = true;
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            if (_done && _blackBoard.moveDirection != Vector3.zero)
            {
                _fsm.ChangeState(_sprintState);
                return StateStatus.Success;
            }
            
            /*if (_done && _blackBoard.dodge)
            {
                _fsm.ChangeState(this);
                return StateStatus.Success;
            }*/

            if (_done && _blackBoard.moveDirection == Vector3.zero)
            {
                _fsm.ChangeState(lowIdleCombat);
                return StateStatus.Success;
            }
            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
            _blackBoard.playerMovement.useRootMotion = false;
            _blackBoard.playerMovement.rootmotionSpeedMult = 1;
            _done = false;
        }

        private ClipTransition HandelDodge()
        {
            if (_blackBoard.moveDirection.y <= 0)
            {
                if (_blackBoard.moveDirection.x is >= -1 and < -0.5f)
                {
                    return _dodgeAnimLeft;
                }

                if (_blackBoard.moveDirection.x is >= -0.5f and <= 0.5f)
                {
                    return _dodgeAnimBack;
                }

                if (_blackBoard.moveDirection.x is > 0.5f and <= 1)
                {
                    return _dodgeAnimRight;
                }
            }
            return _dodgeAnimRight;
        }
    }
}