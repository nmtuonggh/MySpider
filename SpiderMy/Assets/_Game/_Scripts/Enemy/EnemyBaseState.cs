using Animancer;
using System.Collections;
using System.Collections.Generic;
using SFRemastered._Game._Scripts.Enemy.State;
using UnityEngine;

namespace SFRemastered
{
    public enum EnemyStateStatus
    {
        None,
        Success,
        Failure,
        Running
    }

    public abstract class EnemyBaseState : MonoBehaviour
    {
        protected EnemyFSM _enemyFsmfsm;
        protected EnemyBlackBoard _blackBoard;
        protected bool _isAIControlled;
        public bool canTransitionToSelf = false;
        protected float elapsedTime { get; private set; }
        [SerializeField] protected ClipTransition _mainAnimation;
        protected AnimancerState _state;

        public virtual void InitState(EnemyFSM enemyFsm, EnemyBlackBoard blackBoard, bool isAIControlled)
        {
            _enemyFsmfsm = enemyFsm;
            _blackBoard = blackBoard;
            _isAIControlled = isAIControlled;
            elapsedTime = 0;
        }

        public virtual void EnterState()
        {
            elapsedTime = 0;

            if (_mainAnimation.Clip != null)
                _state = _blackBoard.animancer.Play(_mainAnimation);
        }

        public virtual void ConsistentUpdateState()
        {
            elapsedTime += Time.deltaTime;
        }

        public virtual StateStatus UpdateState()
        {
            return StateStatus.Running;
        }

        public virtual void FixedUpdateState() { }

        public virtual void ExitState() { }
    }
}