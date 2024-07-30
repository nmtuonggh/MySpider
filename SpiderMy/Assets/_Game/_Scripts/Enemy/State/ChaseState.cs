
using Animancer;
using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
using NodeCanvas.Tasks.Actions;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Enemy.State
{
    public class ChaseState : ActionTask<Transform>
    {
        [Header("Move Towards")]
        public BBParameter<GameObject> target;
        public BBParameter<float> speed = 2;
        public float stopDistance ;
        [Header("Rotate Towards")]
        public BBParameter<float> rotationSpeed;
        public BBParameter<float> angleDifference = 5;
        
        
        protected override string info {
            get { return "Chase player until distance equal 2 to 5 meter"; }
        }

        protected override void OnExecute()
        {
            base.OnExecute();
            stopDistance = Random.Range(2f, 5f);
        }
        
        protected override void OnUpdate()
        {
            if (( agent.position - target.value.transform.position ).magnitude <= stopDistance ) {
                EndAction();
                return;
            }
            
            var dir = target.value.transform.position - agent.position;
            agent.position = Vector3.MoveTowards(agent.position, target.value.transform.position, speed.value * Time.deltaTime);
            agent.rotation = Quaternion.LookRotation(Vector3.RotateTowards(agent.forward, dir, speed.value * Time.deltaTime, 0), Vector3.up);

        }
    }
}