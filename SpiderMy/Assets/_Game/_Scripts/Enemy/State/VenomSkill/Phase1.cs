using UnityEngine;

namespace SFRemastered.VenomSkill
{
    public class Phase1 : EnemyBaseState
    {
        public override void EnterState()
        {
            base.EnterState();
            var target = _blackBoard.target.obj.transform.position;
            var targetDir = target - transform.position;
            targetDir.y = 0;
            _blackBoard.animancer.Animator.applyRootMotion = true;
            _blackBoard.characterController.transform.rotation = Quaternion.LookRotation(targetDir);
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            if (_state.NormalizedTime >= 1f)
            {
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
            _blackBoard.animancer.Animator.applyRootMotion = false;
        }
    }
}