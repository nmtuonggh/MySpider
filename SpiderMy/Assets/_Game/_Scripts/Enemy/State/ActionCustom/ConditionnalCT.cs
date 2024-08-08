using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using SFRemastered._Game._Scripts.ReferentSO;
using UnityEngine;

namespace SFRemastered
{
    public class ConditionnalCT :ConditionTask<Transform>
    {
        public string stateInfor;

        [RequiredField]
        public BBParameter<GameObjectRef> checkTarget;
        public CompareMethod checkType = CompareMethod.LessThan;
        public BBParameter<float> distance = 10;

        [SliderField(0, 0.1f)]
        public float floatingPoint = 0.05f;

        protected override string info
        {
            get { return stateInfor; }
        }

        protected override bool OnCheck() {
            return OperationTools.Compare(Vector3.Distance(agent.position, checkTarget.value.obj.transform.position), distance.value, checkType, floatingPoint);
        }

        public override void OnDrawGizmosSelected() {
            if ( agent != null ) {
                Gizmos.DrawWireSphere(agent.position, distance.value);
            }
        }
    }
}