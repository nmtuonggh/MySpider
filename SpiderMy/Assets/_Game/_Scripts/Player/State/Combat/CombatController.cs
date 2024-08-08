using SFRemastered._Game._Scripts.State.Combat.ComboAttack;
using SFRemastered.Combat;
using SFRemastered.Combat.ZipAttack;
using UnityEngine;

namespace SFRemastered._Game._Scripts.State.Combat
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/AttackController")]
    public class CombatController : CombatBase
    {
        [SerializeField] private LeapAttack.LeapAttack _leapAttack;
        [SerializeField] private FirstCombo _firstCombo;
        [SerializeField] private SecondCombo _secondCombo;
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
                if (_blackBoard._distanceToTargetEnemy is > 0 and <= 2.5f)
                {
                    //TODO: Random attack combo
                    var random = Random.Range(0, 2);
                    if (random == 0)
                        _fsm.ChangeState(_firstCombo);
                    else
                        _fsm.ChangeState(_secondCombo);
                }
                else if (_blackBoard._distanceToTargetEnemy is > 2.5f and <= 8)
                {
                    _fsm.ChangeState(_leapAttack);
                }
                else if (_blackBoard._distanceToTargetEnemy is > 8 and <= 25)
                {
                    _fsm.ChangeState(_startZipAttack);
                }
                else
                {
                    _fsm.ChangeState(_firstCombo);
                }

                return StateStatus.Success;
            }
            else if (!_blackBoard._detectedEnemy)
            {
                _fsm.ChangeState(_firstCombo);
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }
    }
}