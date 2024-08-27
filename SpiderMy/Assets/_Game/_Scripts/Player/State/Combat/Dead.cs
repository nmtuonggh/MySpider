using Animancer;
using SFRemastered._Game._Scripts.State.Combat;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Player.State.Combat
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/Dead")]
    public class Dead : StateBase
    {
        [SerializeField] private ClipTransition[] _deathClips;
        private int randomIndex;
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.spiderSen.spiderSenCount = 0;
            _blackBoard.dead = false;
            randomIndex = Random.Range(0, _deathClips.Length);
            _state = _blackBoard.animancer.Play(_deathClips[randomIndex]);
            _blackBoard.playerMovement.SetMovementDirection(Vector3.zero);
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            if (_blackBoard.revive)
            {
                _fsm.ChangeState(_blackBoard.stateReference.IdleState);
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }

        public override void ExitState()
        {
            _blackBoard.dead = false;
            base.ExitState();
        }
    }
}