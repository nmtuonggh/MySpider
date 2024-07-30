using NodeCanvas.Framework;
using NodeCanvas.Tasks.Actions;
using ParadoxNotion.Design;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Enemy.State
{
    public class Anim : ActionTask<Animator>
    {
        public BBParameter<int> layerIndex;
        [RequiredField]
        public BBParameter<string> stateName;
        [SliderField(0, 1)]
        public float transitTime = 0.25f;
        public bool waitUntilFinish;
        

        private AnimatorStateInfo stateInfo;
        private bool played;

        protected override string info {
            get { return "Anim '" + stateName.ToString() + "'"; }
        }

        protected override void OnExecute() {
            if ( string.IsNullOrEmpty(stateName.value) ) {
                EndAction();
                return;
            }
            played = false;
            var current = agent.GetCurrentAnimatorStateInfo(layerIndex.value);
            agent.CrossFade(stateName.value, transitTime / current.length, layerIndex.value, 0, 0);
        }

        protected override void OnUpdate() {

            stateInfo = agent.GetCurrentAnimatorStateInfo(layerIndex.value);

            if ( waitUntilFinish ) { 

                if ( stateInfo.IsName(stateName.value) ) {

                    played = true;
                    if ( elapsedTime >= ( stateInfo.length / agent.speed ) ) {
                        EndAction();
                    }

                } else if ( played ) {

                    EndAction();
                }

            } else {

                if ( elapsedTime >= transitTime ) {
                    EndAction();
                }
            }
        }
    }
}