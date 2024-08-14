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
        [SerializeField] protected bool _canGoToNextAttack = false;

        /*private float _currentDamage;*/
        private float time;

        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.playerMovement.rootmotionSpeedMult = _blackBoard._detectedEnemy ? .8f : 1f;
            _blackBoard.playerMovement.useRootMotion = true;
            _currentComboIndex = 0;
            if (_blackBoard._detectedEnemy && _blackBoard.enemyInRange.FindClosestEnemy()!=null)
                _blackBoard.playerMovement.transform.DOLookAt(_blackBoard.enemyInRange.FindClosestEnemy().transform.position, 0.3f,
                    AxisConstraint.Y);
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

            if (_blackBoard.attack && _canGoToNextAttack)
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
            else if(_state.NormalizedTime >= 1f)
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

            if (_blackBoard.enemyInRange.GetDistanceToTargetEnemy() is > 2f)
            {
                _fsm.ChangeState(combatController);
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }

        public void PlayComboAnimation(AttackAnim[] clip, int index)
        {
            _canGoToNextAttack = false;
            _delayTime = clip[index].delayAttack;
            _currentDamage = clip[index].damage;
            _state = _blackBoard.animancer.Play(clip[index].clip);
            _state.Events.Add(0.3f, RotateToTarget);
            _state.Events.Add(0.5f, MoveToTarget);
            time = 0;
        }

        public void CanGoToNextAttack()
        {
            _canGoToNextAttack = true;
        }

        public void RotateToTarget()
        {
            if (_blackBoard._detectedEnemy && _blackBoard.enemyInRange.FindClosestEnemy()!=null)
            {
                _blackBoard.rigidbody.transform.DOLookAt(_blackBoard.enemyInRange.FindClosestEnemy().transform.position, 0.2f,
                    AxisConstraint.Y);
            }
        }
        
        public void MoveToTarget()
        {
            if (_blackBoard._detectedEnemy && _blackBoard.enemyInRange.FindClosestEnemy()!=null)
            {
                var targetPosition = _blackBoard.enemyInRange.FindClosestEnemy().transform.position;
                var direction = _blackBoard.enemyInRange.FindClosestEnemy().transform.position - _blackBoard.playerMovement.transform.position;
                _blackBoard.playerMovement.transform.DOMove(targetPosition - direction.normalized*.8f, 0.2f);
            }
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