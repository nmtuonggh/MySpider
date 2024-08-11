using NodeCanvas.Framework;
using ParadoxNotion;
using SFRemastered._Game._Scripts.ReferentSO;
using UnityEngine;

namespace SFRemastered
{
    public class Attack : EnemyBaseState
    {
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.animancer.Animator.applyRootMotion = true;
        }
        
        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if(baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            if (_state.NormalizedTime >= 1f)
            {
                _blackBoard.animancer.Animator.applyRootMotion = false;
                return StateStatus.Success;
            }
            
            return StateStatus.Running;
        }

        public override void ExitState()
        {
            
            base.ExitState();
        }
    }
}