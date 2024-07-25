using System;
using Animancer;
using UnityEngine;

namespace SFRemastered.Combat
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/FirstCombo")]
    public class FirstCombo : StateBase
    {
        [SerializeField] private ClipTransition[] _comboClips;
        [SerializeField] private ClipTransitionAsset[] _firstComboClips;
        [SerializeField] private IdleCombat _idleCombat;
        [SerializeField] private AttackController _attackController;
        [SerializeField] private int _currentComboIndex = 0;
        [SerializeField] private float _delayTime;
        private float time;

        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.playerMovement.rootmotionSpeedMult = 1;
            _blackBoard.playerMovement.useRootMotion = true;
            _currentComboIndex = 0;
            time = 0;
            PlayCurrentComboAnimation();
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            time += Time.deltaTime;

            if (_blackBoard.attack && time < _delayTime && _state.NormalizedTime >= 0.5f)
            {
                Debug.Log("1");
                if(_currentComboIndex < _comboClips.Length - 1)
                    _currentComboIndex++;
                else
                    _currentComboIndex = 0;
                PlayCurrentComboAnimation();
            }
            else if (time >= _delayTime  )
            {
                Debug.Log("2");

                if (_blackBoard.attack)
                {
                    Debug.Log("3");

                    _fsm.ChangeState(_attackController);
                    return StateStatus.Success;
                }
                else if (_state.NormalizedTime >= 1)
                {
                    Debug.Log("4");
                    _fsm.ChangeState(_idleCombat);
                    return StateStatus.Success;
                }
                
            }

            return StateStatus.Running;
        }

        private void PlayCurrentComboAnimation()
        {
            _state = _blackBoard.animancer.Play(_comboClips[_currentComboIndex]);
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