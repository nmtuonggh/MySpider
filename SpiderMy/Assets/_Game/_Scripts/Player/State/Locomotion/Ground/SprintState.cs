using Animancer;
using System.Collections;
using System.Collections.Generic;
using SFRemastered._Game._Scripts.Player.State.Combat.Gadget;
using SFRemastered._Game._Scripts.State.Combat;
using SFRemastered.Wall;
using UnityEngine;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/Sprint")]
    public class SprintState : GroundState
    {
        //[SerializeField] private WalkState _walkState;
        [SerializeField] private SprintTurn180State _turn180State;
        [SerializeField] private SprintToIdleState _sprintToIdleState;
        [SerializeField] private JumpToSwing _jumpToSwing;
        [SerializeField] private WallRun _wallRun;
        [SerializeField] private LinearMixerTransition _sprintingBlendTree;
        [SerializeField] private WebShooter _webShooter;
        [SerializeField] private UltimateSkill _ultimateSkill;

        public override void EnterState()
        {
            base.EnterState();

            _blackBoard.playerMovement.Sprint();
            _state = _blackBoard.animancer.Play(_sprintingBlendTree);
        }

        public override void ExitState()
        {
            base.ExitState();
            _blackBoard.playerMovement.rotationRate = 540;
            _blackBoard.playerMovement.StopSprinting();
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            _blackBoard.playerMovement.rotationRate = _blackBoard.playerMovement.GetSpeed() >= 6 ? 270 : 540;

            ((LinearMixerState)_state).Parameter = Mathf.Lerp(((LinearMixerState)_state).Parameter, _blackBoard.playerMovement.GetSpeed(), 55 * Time.deltaTime);

            _blackBoard.playerMovement.SetMovementDirection(_blackBoard.moveDirection);

            if(_blackBoard.moveDirection.magnitude < 0.1f)
            {
                _fsm.ChangeState(_sprintToIdleState);
                return StateStatus.Success;
            }

            if(Vector3.Angle(_fsm.transform.forward, _blackBoard.moveDirection) > 150f && elapsedTime > .2f)
            {
                _fsm.ChangeState(_turn180State);
                return StateStatus.Success;
            }
            
            if (_blackBoard.foundWall)
            {
                _fsm.ChangeState(_wallRun);
                return StateStatus.Success;
            }

            if (_blackBoard.swing)
            {
                _fsm.ChangeState(_jumpToSwing);
                return StateStatus.Success;
            }

            if (_blackBoard.ultimate)
            {
                _fsm.ChangeState(_ultimateSkill);
                return StateStatus.Success;
            }
            
            if (_blackBoard.gadget)
            {
                _fsm.ChangeState(_webShooter);
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }
    }
}
