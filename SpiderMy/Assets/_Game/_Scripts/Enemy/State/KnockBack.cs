using DG.Tweening;

namespace SFRemastered
{
    namespace SFRemastered
    {
        public class KnockBack : EnemyBaseState
        { 
            public override void EnterState()
            {
                base.EnterState();
                var position = _blackBoard.target.obj.transform.position;
                var direction =_blackBoard.characterController.transform.position - position;
                //_blackBoard.characterController.transform.DOLookAt(position,0.1f);
                //_blackBoard.characterController.transform.DOMove(direction.normalized * 5,0.1f);
                _blackBoard.cantTarget = true;
                //_blackBoard.disableRB = true;
                _blackBoard.lineRenderer.positionCount = 0;
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
                    _blackBoard.cantTarget = false;
                    //_blackBoard.disableRB = false;
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
}