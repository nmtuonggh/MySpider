using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Player.State.Combat.Gadget
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/GadgetAdapter")]
    public class GadgetAdapter : StateBase
    {
        //[SerializeField] private int gadgetIndex;
        [SerializeField] public List<StateBase> listGadgetStates;
        public override void EnterState()
        {
            base.EnterState();
            
            _fsm.ChangeState(listGadgetStates[_blackBoard.gadgetIndex]);
        }
        
        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public void SetGadgetIndex(int x)
        {
            _blackBoard.gadgetIndex = x;
        }
    }
}