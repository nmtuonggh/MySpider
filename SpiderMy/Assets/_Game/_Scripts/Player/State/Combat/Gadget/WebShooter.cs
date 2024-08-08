using DG.Tweening;
using SFRemastered._Game._Scripts.State.Combat;
using SFRemastered._Game._Scripts.State.Combat.IdleCombat;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Player.State.Combat.Gadget
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/WebShooter")]
    public class WebShooter : CombatBase
    {
        [SerializeField] private NormalIdleCombat _normalIdleCombat;
        
        public override void EnterState()
        {          
            _blackBoard.playerMovement.transform.DOLookAt(_blackBoard._closestEnemyNotStun.transform.position, 0.2f, AxisConstraint.Y);
            base.EnterState();
        }
        
        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if(baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            if (_state.NormalizedTime >= 0.9f)
            {
                _fsm.ChangeState(_normalIdleCombat);
                return StateStatus.Success;
            }
            
            return StateStatus.Running; 
        }
        
        public void ShootWebLeft()
        {
            var rotation = Quaternion.LookRotation(_blackBoard._targetEnemy.transform.position - _blackBoard.playerMovement.transform.position);
            _blackBoard.projectileData.Spawn(_blackBoard.startrope.position, rotation, _blackBoard.poolManager.transform);
        }
        
        public void ShootWebRight()
        {
            var rotation = Quaternion.LookRotation(_blackBoard._targetEnemy.transform.position - _blackBoard.playerMovement.transform.position);
            _blackBoard.projectileData.Spawn(_blackBoard._zipAttackHandPositon.position, rotation, _blackBoard.poolManager.transform);
        }
        
        public override void ExitState()
        {
            base.ExitState();
        }
    }
}