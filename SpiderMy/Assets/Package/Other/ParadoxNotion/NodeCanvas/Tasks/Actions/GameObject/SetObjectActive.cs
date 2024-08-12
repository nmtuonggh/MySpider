
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions
{

    [Name("Set Active")]
    [Category("GameObject")]
    [Description("Set the gameobject active state.")]
    public class SetObjectActive : ActionTask<Transform>
    {

        [RequiredField]
        public BBParameter<GameObject> obj;

        public enum SetActiveMode
        {
            Deactivate = 0,
            Activate = 1,
            Toggle = 2
        }

        public SetActiveMode setTo = SetActiveMode.Toggle;

        protected override string info {
            get { return string.Format("{0} {1}", setTo, obj); }
        }

        protected override void OnExecute() {

            bool value;

            if ( setTo == SetActiveMode.Toggle ) {

                value = !agent.gameObject.activeSelf;

            } else {

                value = (int)setTo == 1;
            }

            obj.value.SetActive(value);
            EndAction();
        }
    }
}