
using NodeCanvas.Editor;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SFRemastered._Game._Scripts.Enemy.State;
using SFRemastered._Game._Scripts.ReferentSO;
using UnityEngine;


namespace SFRemastered {

    [Category("GeneralAction")]
    [Description("Chase custom state")]
    public class ChaseAction : ActionTask<EnemyFSM> {

        public EnemyBaseState state;
        [RequiredField]
        public BBParameter<GameObjectRef> target;
        public float minDistance;
        public float maxDistance;
        private float randomDistance;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
            return null;
        }
		
        protected override string info {
            get { return "Chase the target until "+minDistance + " to " +maxDistance + "m"; }
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute() {
            randomDistance = Random.Range(minDistance, maxDistance);
            agent.ChangeState(state, true);
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate() {
            StateStatus result = agent.OnUpdate();
            if(result == StateStatus.Success) {
                EndAction(true);
            }
            else if(result == StateStatus.Failure) {
                EndAction(false);
            }
            

            if(Vector3.Distance(agent.transform.position, target.value.obj.transform.position) <= randomDistance) {
                EndAction(true);
            }
        }

        //Called when the task is disabled.
        protected override void OnStop() {
			
        }

        //Called when the task is paused.
        protected override void OnPause() {
			
        }
    }
}