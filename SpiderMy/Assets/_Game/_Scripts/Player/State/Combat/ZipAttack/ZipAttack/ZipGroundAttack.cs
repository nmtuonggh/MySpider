using DG.Tweening;
using EasyCharacterMovement;
using SFRemastered._Game._Scripts.Enemy;
using SFRemastered._Game._Scripts.Enemy.State;
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
            var distance = _blackBoard.enemyInRange.FindClosestEnemy().gameObject.GetComponent<EnemyBlackBoard>().enemyData.enemyType == EnemySO.EnemyType.Boss? 0.5f : 0.25f;
            var targetDrawnWeb =
                _blackBoard.enemyInRange.FindClosestEnemy().GetComponent<CharacterController>().height * 2 / 3;
            DrawnWeb(_blackBoard._zipAttackHandPositon.transform.position,
                _blackBoard.enemyInRange.FindClosestEnemy().transform.position + new Vector3(0, targetDrawnWeb, 0));

            _blackBoard.playerMovement.transform
                .DOMove((_blackBoard.enemyInRange.FindClosestEnemy().transform.position + targetPos * distance) + new Vector3(0, 0.05f, 0), 0.35f)
                .OnComplete(
                    () =>
                    {
                        _doneMove = true;
                        _blackBoard.lr.positionCount = 0;
                    });
        }
    }
}