using Animancer;
using UnityEngine;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/Dive")]
    public class DiveState : FallDownBaseState
    {
        //[SerializeField] private LinearMixerTransition _diveBlendTree;
        public override void EnterState()
        {
            base.EnterState();
            //_state = _blackBoard.animancer.Play(_diveBlendTree);
        }

        public override void ExitState()
        {
            base.ExitState();
        }


        public override StateStatus UpdateState()
        {
            
            //((LinearMixerState)_state).Parameter = Mathf.Lerp(((LinearMixerState)_state).Parameter, _blackBoard.playerMovement.GetVelocity().magnitude, 55 * Time.deltaTime);
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            return StateStatus.Running;
        }
    }
}