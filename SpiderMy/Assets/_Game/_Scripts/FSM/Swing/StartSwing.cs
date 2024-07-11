using UnityEngine;

namespace SFRemastered.Swing
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/StartSwing")]
    public class StartSwing: SwingState
    {
        public override void EnterState()
        {
            base.EnterState();
             
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            return StateStatus.Running;
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
            DrawRope();
        }
        
        public override void ExitState()
        {
            base.ExitState();
            Destroy(_springJoint);
            _blackBoard.lr.positionCount = 0;
            _blackBoard.swing = false;
        }
        
        public void startSwing()
        {
            swingPoint.position = _blackBoard.swingPoint2.position;
            _springJoint = _blackBoard.playerMovement.gameObject.AddComponent<SpringJoint>();
            _springJoint.autoConfigureConnectedAnchor = false;
            _springJoint.connectedAnchor = swingPoint.position;
            float distanceFromPoint = Vector3.Distance(_blackBoard.playerPoint.position, swingPoint.position);
            
            _springJoint.maxDistance = distanceFromPoint * 0.8f;
            _springJoint.minDistance = distanceFromPoint * 0.25f;
            
            _springJoint.spring = 4.5f;
            _springJoint.damper = 7f;
            _springJoint.massScale = 4.5f;
            
            _blackBoard.lr.positionCount = 2;
            GrapPoint = _blackBoard.playerPoint;
        }

        private Transform GrapPoint;
        void DrawRope() {
            if (!_springJoint) return;

            //GrapPoint.position = Vector3.Lerp(GrapPoint.position, _blackBoard.swingPoint.position, Time.deltaTime * 8f);
            _blackBoard.lr.SetPosition(0, _blackBoard.playerPoint.position);
            _blackBoard.lr.SetPosition(1, swingPoint.position);
        }
        
    }
}