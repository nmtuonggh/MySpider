using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;

namespace SFRemastered.Combat.ZipAttack
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/ZipAttack")]
    public class ZipGroundAttack : ZipAttack
    {
        public override void EnterState()
        {
            base.EnterState();
            var targetPos = (_blackBoard.transform.position - _blackBoard.enemyInRange.FindClosestEnemy().transform.position).normalized;

            DrawnWeb(_blackBoard._zipAttackHandPositon.transform.position,
                _blackBoard.enemyInRange.FindClosestEnemy().transform.position + new Vector3(0, _blackBoard._zipAttackHandPositon.transform.position.y, 0));

            _blackBoard.playerMovement.transform
                .DOMove((_blackBoard.enemyInRange.FindClosestEnemy().transform.position + targetPos * .3f) + new Vector3(0, 0.05f, 0), 0.35f)
                .OnComplete(
                    () =>
                    {
                        _doneMove = true;
                        _blackBoard.lr.positionCount = 0;
                    });
        }
    }
}