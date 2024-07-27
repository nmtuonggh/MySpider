using Animancer;
using DG.Tweening;
using SFRemastered._Game._Scripts.State.Combat.IdleCombat.SFRemastered.Combat;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered._Game._Scripts.State.Combat.ComboAttack
{
    public abstract class ComboAttackBase : CombatBase
    {
        [SerializeField] protected ClipTransitionAsset[] _firstComboClips;
        [SerializeField] protected ClipTransitionAsset[] _extraAttackClips;
        
        [SerializeField] protected IdleCombat.NormalIdleCombat normalIdleCombat;
        [SerializeField] protected LowIdleCombat lowIdleCombat;
        [SerializeField] protected CombatController combatController;
        
        [SerializeField] protected int _currentComboIndex = 0;
        [SerializeField] protected float _delayTime;
        
        private float time;

        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.playerMovement.rootmotionSpeedMult = 1;
            _blackBoard.playerMovement.useRootMotion = true;
            _currentComboIndex = 0;
            time = 0;
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

        public void PlayComboAnimation(ClipTransitionAsset[] clip, int index)
        {
            _blackBoard.playerMovement.RotateTowardsWithSlerp(new Vector3(0, _blackBoard._targetEnemy.transform.position.y, 0));
            _state = _blackBoard.animancer.Play(clip[index]);
            time = 0;
        }
        

        public override void ExitState()
        {
            base.ExitState();
            _currentComboIndex = 0;
            _blackBoard.playerMovement.rootmotionSpeedMult = 1;
            _blackBoard.playerMovement.useRootMotion = false;
        }
    }
}