using Animancer;
using SFRemastered._Game._Scripts.State.Combat.IdleCombat.SFRemastered.Combat;
using UnityEngine;

namespace SFRemastered._Game._Scripts.State.Combat.ComboAttack
{
    public abstract class ComboAttackBase : StateBase
    {
        [SerializeField] protected ClipTransitionAsset[] _firstComboClips;
        [SerializeField] protected ClipTransitionAsset[] _extraAttackClips;
        
        [SerializeField] protected IdleCombat.IdleCombat _idleCombat;
        [SerializeField] protected LowIdleCombat _lowIdleCombat;
        [SerializeField] protected AttackController _attackController;
        
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
                    _fsm.ChangeState(_attackController);
                
                else
                {
                    if (_currentComboIndex < 3)
                        _fsm.ChangeState(_idleCombat);
                    else
                        _fsm.ChangeState(_lowIdleCombat);
                }
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }

        public void PlayComboAnimation(ClipTransitionAsset[] clip, int index)
        {
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