
using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Enemy.State
{
    public class PatrollState : ActionNode 
    {
        protected override Status OnExecute(Component agent, IBlackboard blackboard)
        {
            Debug.Log("OnExecute PatrollState");
            action.Execute(agent, blackboard);
            return Status.Success;
        }
    }
}