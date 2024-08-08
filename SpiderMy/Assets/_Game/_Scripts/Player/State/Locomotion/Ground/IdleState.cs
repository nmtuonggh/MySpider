 using System.Collections;
 using SFRemastered._Game._Scripts.Player.State.Combat.Gadget;
 using SFRemastered._Game._Scripts.State.Combat;
 using SFRemastered._Game._Scripts.State.Combat.IdleCombat;
 using SFRemastered.Combat;
 using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/Idle")]
    public class IdleState : GroundState
    {
        [SerializeField] private SprintState _sprintState;
        [SerializeField] private JumpToSwing _jumpToSwing;
        [FormerlySerializedAs("normalIdleCombatBase")] [FormerlySerializedAs("idleIdlesCombat")] [FormerlySerializedAs("_idleCombat")] [SerializeField] private NormalIdleCombat normalIdleCombat;
        [SerializeField] private WebShooter _webShooter;
        [SerializeField] private UltimateSkill _ultimateSkill;
        public override void EnterState()
        {
            base.EnterState();

            _blackBoard.playerMovement.SetMovementDirection(Vector3.zero);
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if(baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            if(_blackBoard.moveDirection.magnitude > 0f)
            {
                _fsm.ChangeState(_sprintState);
                return StateStatus.Success;
            }

            if (_blackBoard.swing)
            {
                _fsm.ChangeState(_jumpToSwing);
                return StateStatus.Success;
            }

            if (_blackBoard.attack)
            {
                _fsm.ChangeState(normalIdleCombat);
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