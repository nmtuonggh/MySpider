using Animancer;
using DG.Tweening;
using UnityEngine;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/ZipEnd")]

    public class ZipEnd : ZipBase
    {
        [SerializeField] private SprintState _sprintState;

        public override void EnterState()
        {
            base.EnterState();
        }

        public override StateStatus UpdateState()
        {
            base.UpdateState();
            
            if (_blackBoard.moveDirection != Vector3.zero)
            {
                _fsm.ChangeState(_sprintState);
                return StateStatus.Success;
            }
            
            if (_blackBoard.zip && _blackBoard.raycastCheckWall.zipPoint!=Vector3.zero)
            {
                _fsm.ChangeState(_blackBoard.stateReference.StartZip);
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