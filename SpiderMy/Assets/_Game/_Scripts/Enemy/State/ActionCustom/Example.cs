using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SFRemastered._Game._Scripts.Enemy.State;
using UnityEngine;


namespace SFRemastered {

	[Category("GeneralAction")]
	[Description("Run a state")]
	public class ExampleState : ActionTask<EnemyFSM> {

		public EnemyBaseState state;
		public float time;
		public string stateInfor;
		
		private float currentTime = 0;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}
		
		protected override string info {
			get { return stateInfor; }
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			agent.ChangeState(state, true);
			currentTime = 0;
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			currentTime += Time.deltaTime;
			StateStatus result = agent.OnUpdate();
			if(result == StateStatus.Success) {
                EndAction(true);
            }
            else if(result == StateStatus.Failure) {
                EndAction(false);
            }

			if (time!=0 && time>=currentTime) {
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