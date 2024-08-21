using UnityEngine;

namespace SFRemastered
{
    public class ZipBase: StateBase
    {
        public Vector3 currentZipPoint;
        public override void EnterState()
        {
            base.EnterState();
        }

        public override StateStatus UpdateState()
        {
            base.UpdateState();

            return StateStatus.Running;
        }
        
        public override void ExitState()
        {
            
            base.ExitState();
        }
    }
}