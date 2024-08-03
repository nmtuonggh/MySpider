using Animancer;
using DG.Tweening;
using SFRemastered._Game._Scripts.State.Combat.IdleCombat.SFRemastered.Combat;
using SFRemastered._Game.ScriptableObjects.AnimationAttack;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered._Game._Scripts.State.Combat.ComboAttack
{
    public abstract class ComboAttackBase : CombatBase
    {
        [SerializeField] protected AttackAnim[] _firstComboClips;
        [SerializeField] protected AttackAnim[] _extraAttackClips;
        
        [SerializeField] protected IdleCombat.NormalIdleCombat normalIdleCombat;
        [SerializeField] protected LowIdleCombat lowIdleCombat;
        [SerializeField] protected CombatController combatController;
        
        [SerializeField] protected int _currentComboIndex = 0;
        [SerializeField] protected float _delayTime;
        
        /*private float _currentDamage;*/
        private float time;

        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.playerMovement.rootmotionSpeedMult = _blackBoard._detectedEnemy ? .5f : 1f;
            _blackBoard.playerMovement.useRootMotion = true;
            _currentComboIndex = 0;
            time = 0;
            if (_blackBoard._detectedEnemy)
                _blackBoard.playerMovement.transform.DOLookAt(_blackBoard._targetEnemy.transform.position, 0.3f, AxisConstraint.Y); 
            PlayComboAnimation(_firstComboClips, _currentComboIndex);
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            time += Time.deltaTime;
            
            //TODO: replace _state.NormalizedTime by attackanim.duration
            
            if (_blackBoard.attack && time < _delayTime && 
                (_currentComboIndex < 3 ? _state.NormalizedTime >= 0.5f : _state.NormalizedTime >= 1f))
            {
                if (_currentComboIndex < 3)
                    _currentComboIndex++;
                else
                    _currentComboIndex = 0;

                if (_currentComboIndex < 3)
                    PlayComboAnimation(_firstComboClips, _currentComboIndex);
                else
                    PlayComboAnimation(_extraAttackClips, Random.Range(0, _extraAttackClips.Length));
            }
            else if (time >= _delayTime  )
            {
                if (_blackBoard.attack)
                    _fsm.ChangeState(combatController);
                
                else
                {
                    if (_currentComboIndex < 3)
                        _fsm.ChangeState(normalIdleCombat);
                    else
                        _fsm.ChangeState(lowIdleCombat);
                }
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }

        public void PlayComboAnimation(AttackAnim[] clip, int index)
        {
            //TODO: rotate player to enemy
            
            _currentDamage = clip[index].damage;
            _state = _blackBoard.animancer.Play(clip[index].clip);

            if (_blackBoard._detectedEnemy)
            {
                _state.Events.SetShouldNotModifyReason(null);
                _state.Events.SetCallback("Hit", GetHit);
            }

            time = 0;
        }
        
        /*public void GetHit()
        {
            _blackBoard.overlapSphereHit.Hit(_currentDamage);
        }*/

        public override void ExitState()
        {
            base.ExitState();
            _currentComboIndex = 0;
            _blackBoard.playerMovement.rootmotionSpeedMult = 1;
            _blackBoard.playerMovement.useRootMotion = false;
        }
    }
}