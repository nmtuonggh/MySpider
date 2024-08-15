using Animancer;
using UnityEngine;

namespace SFRemastered
{
    public class StunLock : EnemyBaseState
    {
        [SerializeField] private ClipTransition startStunLock;
        [SerializeField] private ClipTransition loopStunLock;
        [SerializeField] private ClipTransition endStunLock;
        
        private float currentTime;
        
        public override void EnterState()
        {
            base.EnterState();
            currentTime = 0;
            _blackBoard.lineRenderer.positionCount = 0;
            _state = _blackBoard.animancer.Play(startStunLock);
            _blackBoard.animancer.Animator.applyRootMotion = true;
        }
        

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if(baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            currentTime += Time.deltaTime;
            
            if (_state.NormalizedTime >= .95f && _state.Clip == startStunLock.Clip)
            {
                _state = _blackBoard.animancer.Play(loopStunLock);
            }
            if (currentTime >= _blackBoard.stunLockTime - endStunLock.Length && _state.Clip == loopStunLock.Clip)  
            {
                _state = _blackBoard.animancer.Play(endStunLock);
            }
            
            if (currentTime >= _blackBoard.stunLockTime)
            {
                return StateStatus.Success;
            }
            
            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
            _blackBoard.stunLockHit = false;
            _blackBoard.animancer.Animator.applyRootMotion = false;
        }
    }
}