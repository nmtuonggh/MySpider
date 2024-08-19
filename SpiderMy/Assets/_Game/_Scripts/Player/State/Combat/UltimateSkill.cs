using Animancer;
using SFRemastered._Game._Scripts.Enemy;
using SFRemastered._Game._Scripts.Player.State.Combat.Gadget;
using SFRemastered._Game._Scripts.State.Combat.IdleCombat;
using SFRemastered._Game.ScriptableObjects.AnimationAttack;
using UnityEngine;

namespace SFRemastered._Game._Scripts.State.Combat
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/UltimateSkill")]
    public class UltimateSkill : GadgetBase
    {
        [SerializeField] private AttackAnim anim;
        [SerializeField] private NormalIdleCombat _normalIdleCombat;
        [SerializeField] private LayerMask layer;
        
        public override void EnterState()
        {
            _state = _blackBoard.animancer.Play(anim.clip);
            base.EnterState();
            if (currentStack > 0)
            {
                currentStack--;
                currentCoolDown = coolDown;
            }
            _blackBoard.invincible = true;
            /*else
            {
                _fsm.ChangeState(_normalIdleCombat);
            }*/
            _blackBoard.playerMovement.useRootMotion = true;
            _blackBoard.ultimateEffectPrefab.gameObject.SetActive(true);
            _state.Events.SetShouldNotModifyReason(null);
            _state.Events.OnEnd = () =>
            {
                _fsm.ChangeState(_normalIdleCombat);
            };
        }
        
        public override StateStatus UpdateState()
        {
            
            return StateStatus.Running;
        }
        
        public void BumChiu()
        {
            _blackBoard.ultimateEffectPrefab.Play();
        }

        public void HitDame()
        {
            _blackBoard.overlapSphereHit.UltimateHit(_blackBoard.ultimateEffectPrefab.gameObject.transform, anim.damage);
        }
        
        public override void ExitState()
        {
            base.ExitState();
            _blackBoard.invincible = false;
            _blackBoard.ultimateEffectPrefab.gameObject.SetActive(false);
            _blackBoard.playerMovement.useRootMotion = false;
        }
    }
}