using UnityEngine;

namespace SFRemastered.Combat
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/AttackController")]
    public class AttackController : StateBase
    {
        [SerializeField] private LeapAttack _leapAttack;
        [SerializeField] private FirstCombo _firstCombo;
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
                if (_blackBoard._distanceToTargetEnemy is > 0 and <= 1)
                {
                    Debug.Log("random attack combo");
                }else if (_blackBoard._distanceToTargetEnemy is > 1 and <= 5)
                {
                    Debug.Log("leapAttack");
                    _fsm.ChangeState(_leapAttack);
                    return StateStatus.Success;
                }else if (_blackBoard._distanceToTargetEnemy is > 5 and <= 29)
                {
                    Debug.Log("zip attack");
                }
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