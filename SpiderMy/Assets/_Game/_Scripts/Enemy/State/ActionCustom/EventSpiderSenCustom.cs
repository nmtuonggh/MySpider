using _Game.Scripts.Event;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SFRemastered._Game._Scripts.Enemy.State;
using UnityEngine;


namespace SFRemastered {

    [Category("GeneralAction")]
    [Description("Run a state")]
    public class EventSpiderSenCustom : ActionTask
    {

        public GameEvent gameEvent;
        public BBParameter<GameObject> warningAttack;
        public enum SetActiveMode
        {
            Deactivate = 0,
            Activate = 1,
            Toggle = 2
        }

        public SetActiveMode setTo = SetActiveMode.Toggle;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
            return null;
        }
		
        protected override string info
        {
            get { return "Raise + "; }
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute() {
            bool value;

            if ( setTo == SetActiveMode.Toggle ) {

                value = !agent.gameObject.activeSelf;

            } else {

                value = (int)setTo == 1;
            }
            
            gameEvent.Raise();
            warningAttack.value.SetActive(value);
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate() {
            EndAction(true);
        }

        //Called when the task is disabled.
        protected override void OnStop() {
			
        }

        //Called when the task is paused.
        protected override void OnPause() {
			
        }
    }
} 