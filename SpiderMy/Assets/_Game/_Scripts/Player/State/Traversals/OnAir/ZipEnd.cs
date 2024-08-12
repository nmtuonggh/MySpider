using Animancer;
using DG.Tweening;
using UnityEngine;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/ZipEnd")]

    public class ZipEnd : StateBase
    {
        [SerializeField] private SprintState _sprintState;

        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        }

        public override StateStatus UpdateState()
        {
            base.UpdateState();
            
            if (_blackBoard.moveDirection != Vector3.zero)
            {
                _fsm.ChangeState(_sprintState);
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