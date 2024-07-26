using SFRemastered.Combat.ZipAttack;
using UnityEngine;

namespace SFRemastered.Combat
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/AttackController")]
    public class AttackController : StateBase
    {
        [SerializeField] private LeapAttack _leapAttack;
        [SerializeField] private FirstCombo _firstCombo;
        [SerializeField] private StartZipAttack _startZipAttack;
        
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.playerMovement.SetMovementDirection(Vector3.zero);
             
        }
        public override StateStatus UpdateState()
        {
            base.UpdateState();

            if (_blackBoard._detectedEnemy)
            {
                if (_blackBoard._distanceToTargetEnemy is > 0 and <= 3)
                {
                    Debug.Log("random attack combo");
                    _fsm.ChangeState(_firstCombo);
                }else if (_blackBoard._distanceToTargetEnemy is > 3 and <= 10)
                {
                    Debug.Log("leapAttack");
                    _fsm.ChangeState(_leapAttack);
                }
                else if(_blackBoard._distanceToTargetEnemy is > 10 and <= 29)
                {
                    _fsm.ChangeState(_startZipAttack);
                }
                else
                {
                    _fsm.ChangeState(_firstCombo);
                }
                return StateStatus.Success;
            }else if (!_blackBoard._detectedEnemy)
            {
                Debug.Log("no enemy detected  -  Run ramdom attack combo");
                _fsm.ChangeState(_firstCombo);
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }
    }
}