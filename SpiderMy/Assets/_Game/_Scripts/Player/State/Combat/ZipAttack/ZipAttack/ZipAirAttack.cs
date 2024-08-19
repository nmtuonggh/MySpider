using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;

namespace SFRemastered.Combat.ZipAttack
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/ZipAirAttack")]
    public class ZipAirAttack : ZipAttack
    {
        public override void EnterState()
        {
            base.EnterState();
            
            var playerPlanPos = new Vector3(_blackBoard.transform.position.x, _blackBoard.enemyInRange.FindClosestEnemy().transform.position.y, _blackBoard.transform.position.z);
            var targetPos = (playerPlanPos - _blackBoard.enemyInRange.FindClosestEnemy().transform.position).normalized;

            DrawnWeb(_blackBoard._zipAttackHandPositon.transform.position, (_blackBoard.enemyInRange.FindClosestEnemy().transform.position) + new Vector3(0, 0.4f, 0));

            _blackBoard.playerMovement.transform.DOMove((_blackBoard.enemyInRange.FindClosestEnemy().transform.position + targetPos * .3f) + new Vector3(0, 0.05f, 0), 0.35f)
                .OnComplete(() => { _doneMove = true; _blackBoard.lr.positionCount = 0; });
        }
    }
}