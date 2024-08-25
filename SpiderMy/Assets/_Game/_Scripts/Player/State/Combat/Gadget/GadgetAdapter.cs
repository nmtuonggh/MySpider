using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Player.State.Combat.Gadget
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/GadgetAdapter")]
    public class GadgetAdapter : StateBase
    {
        [SerializeField] public List<GadgetBase> listGadgetStates;
        [SerializeField] public int gadgetIndex;
        [SerializeField] public IdleState idleState;
        
        public override void EnterState()
        {
            base.EnterState();
            Debug.Log("adapter");
            if (listGadgetStates[gadgetIndex].currentStack > 0)
            {
                Debug.Log("current index " + gadgetIndex + "curreent stack " +listGadgetStates[gadgetIndex].currentStack);
                listGadgetStates[gadgetIndex].currentStack--;
                listGadgetStates[gadgetIndex].currentCoolDown = listGadgetStates[gadgetIndex].coolDown;
                _fsm.ChangeState(listGadgetStates[gadgetIndex]);
            }
            else
            {
                _fsm.ChangeState(idleState);
            }
            
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
            _blackBoard.gadget = false;
        }

        public void SetGadgetIndex(int x)
        {
            gadgetIndex = x;
            _blackBoard.gadgetIndex = gadgetIndex;
        }
    }
}