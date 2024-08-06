using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SFRemastered._Game._Scripts.Enemy.State;
using UnityEngine;


namespace SFRemastered {

    [Category("GeneralAction")]
    [Description("Run a state")]
    public class IdleACtion : ActionTask<EnemyFSM> {

        public EnemyBaseState state;

        public float fixedTime;
        public float timeRandom;
        private float currentTime;
        private float randomTime;
        

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
            return null;
        }
		
        protected override string info {
            get
            {
                if (fixedTime > 0)
                {
                    return "Idle to " + fixedTime +"s";
                }
                else
                {
                    return "Idle for 0 to " + timeRandom +"s";
                }
            }
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute() {
            currentTime = 0;
            randomTime = Random.Range(0, timeRandom);
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
            
            currentTime += Time.deltaTime;

            if (fixedTime > 0)
            {
                if (currentTime >= fixedTime)
                {
                    EndAction(true);
                }
            }
            else if (fixedTime <=0)
            {
                if (currentTime >= randomTime)
                {
                    EndAction(true);
                }
            }
            else
            {
                return;
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