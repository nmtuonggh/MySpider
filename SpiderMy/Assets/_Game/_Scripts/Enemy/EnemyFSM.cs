using System.Collections.Generic;
using NodeCanvas.BehaviourTrees;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Enemy.State
{
    public class EnemyFSM : MonoBehaviour
    {
        [SerializeField] private bool _isAIControlled = false;
        [SerializeField] private EnemyBaseState _startingState;
        [SerializeField] private EnemyBaseState _currentState;
        [SerializeField] private EnemyBaseState _previousState;
        private EnemyBlackBoard _blackBoard;

        [SerializeReference] private List<EnemyBaseState> _states;

        public void InitFSM(bool isAIControlled)
        {
            _blackBoard = GetComponent<EnemyBlackBoard>();
            _currentState = _startingState;
            isAIControlled = GetComponent<BehaviourTreeOwner>() != null;
            foreach (var state in _states)
            {
                state.InitState(this, _blackBoard, isAIControlled);
            }
            _currentState.EnterState();
        }

        public bool ChangeState(EnemyBaseState newState, bool force = false)
        {
            if (_isAIControlled && !force)
            {
                return false;
            }

            if (newState == null)
            {
                return false;
            }

            if (_currentState == newState && !newState.canTransitionToSelf)
            {
                return false;
            }

            if (_currentState != null)
            {
                _currentState.ExitState();
            }

            _previousState = _currentState;
            _currentState = newState;
            _currentState.EnterState();

            return true;
        }

        private void Start()
        {
            Application.targetFrameRate = 60;
            InitFSM(_isAIControlled);
        }

        private void Update()
        {
            if (!_isAIControlled)
                OnUpdate();
        }

        public StateStatus OnUpdate()
        {
            if (_currentState == null) return StateStatus.Failure;

            _currentState.ConsistentUpdateState();
            return _currentState.UpdateState();
        }

        private void FixedUpdate()
        {
            if (!_isAIControlled)
                OnFixedUpdate();
        }

        public void OnFixedUpdate()
        {
            if (_currentState == null) return;

            _currentState.FixedUpdateState();
        }

        private void OnDestroy()
        {
            if (_currentState != null)
            {
                _currentState.ExitState();
            }
        }
    }
}