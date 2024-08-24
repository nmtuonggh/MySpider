using Animancer;
using DG.Tweening;
using SFRemastered._Game._Scripts.Enemy.State;
using SFRemastered._Game._Scripts.State.Combat.IdleCombat.SFRemastered.Combat;
using SFRemastered._Game.ScriptableObjects.AnimationAttack;
using SFRemastered.OnHitState;
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
        [SerializeField] protected KnockBackState knockBack;
        [SerializeField] protected LeapAttack.LeapAttack leapAttack;

        [SerializeField] protected int _currentComboIndex = 0;
        [SerializeField] protected bool _canGoToNextAttack = false;

        public override void EnterState()
        {
            base.EnterState();
            _currentComboIndex = 0;
            _blackBoard.rigidbody.interpolation = RigidbodyInterpolation.None;
            _blackBoard.playerMovement.useRootMotion = true;
            //_blackBoard.playerMovement.SetRotationMode(EasyCharacterMovement.RotationMode.None);
            PlayComboAnimation(_firstComboClips, _currentComboIndex);
            _state.Time = 0;
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            //FaceToEnemy();
            
            if (_blackBoard.attack && _canGoToNextAttack)
            {
                if ( _blackBoard.enemyInRange.GetDistanceToClosetEnemy()> 2f)
                {
                    _fsm.ChangeState(combatController);
                    return StateStatus.Success;
                }
                
                if (_currentComboIndex == 3)
                {
                    _fsm.ChangeState(combatController);
                    return StateStatus.Success;
                }
                else
                {
                    _currentComboIndex++;
                }

                if (_currentComboIndex < 3)
                {
                    PlayComboAnimation(_firstComboClips, _currentComboIndex);
                }
                else
                {
                    PlayComboAnimation(_extraAttackClips, Random.Range(0, _extraAttackClips.Length));
                }
            }
            else if (_state.NormalizedTime >= 1f)
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

            if (_blackBoard.knockBackHit)
            {
                _fsm.ChangeState(knockBack);
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }
        
        public override void ExitState()
        {
            base.ExitState();
            _blackBoard.rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            _currentComboIndex = 0;
            _blackBoard.playerMovement.rootmotionSpeedMult = 1;
            _blackBoard.playerMovement.useRootMotion = false;
            //_blackBoard.playerMovement.SetRotationMode(EasyCharacterMovement.RotationMode.OrientToMovement);
        }

        private void PlayComboAnimation(AttackAnim[] clip, int index)
        {
            RotateToTarget();
            HandlerSpeedRootMotion();
            //_state.Events.Add(0.1f, RotateToTarget);
            _canGoToNextAttack = false;
            _currentDamage = clip[index].damage;
            _state = _blackBoard.animancer.Play(clip[index].clip);
        }

        public void CanGoToNextAttack()
        {
            _canGoToNextAttack = true;
        }

        private void RotateToTarget()
        {
            if (_blackBoard._detectedEnemy && _blackBoard.enemyInRange.FindClosestEnemy() != null)
            { 
                _blackBoard.playerMovement.transform.DOLookAt(_blackBoard.enemyInRange.FindClosestEnemy().transform.position,
                    0.1f,
                    AxisConstraint.Y);
            }
        }

        private void HandlerSpeedRootMotion()
        {
            if (_blackBoard.enemyInRange.GetDistanceToClosetEnemy() is > 0 and <= 1f)
            {
                _blackBoard.playerMovement.rootmotionSpeedMult = 0.15f;
            }
            else if (_blackBoard.enemyInRange.GetDistanceToClosetEnemy() is > 1f and <= 2)
            {
                _blackBoard.playerMovement.rootmotionSpeedMult = 0.5f;
            }
        }
    }
}