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
            HandelForce();
            DrawLine();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            return StateStatus.Running;
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
        }
        
        public override void ExitState()
        {
            base.ExitState();
            Destroy(_springJoint);
            _blackBoard.lr.positionCount = 0;
            _blackBoard.swing = false;
        }

        public void HandelForce()
        {
            var v = _blackBoard.playerMovement.GetVelocity();
            var r = _blackBoard.swingPoint.position - _blackBoard.playerSwingPoint.position;
            var tensionMagnitude = v.magnitude * v.magnitude / r.magnitude; // T = (m*v*v)/r
            var tensionDirection = r.normalized;
            
            var gravity = _blackBoard.playerMovement.GetGravityVector();
            
            var gPara = Vector3.Project(gravity, tensionDirection);
            var gPerp = gravity - gPara;
            
            var angle = Vector3.Angle(tensionDirection, gravity);
            var tension = tensionMagnitude * Mathf.Cos(angle);
            var gperpMag = gPerp.magnitude * Mathf.Sin(angle);
            
            //var force =(tension + gperpMag);
            _blackBoard.playerMovement.AddForce( tensionDirection * tension);
            _blackBoard.playerMovement.AddForce( gperpMag * gPerp);
        }
        
        public void DrawLine()
        {
            _blackBoard.lr.positionCount = 2;
            _blackBoard.lr.SetPosition(0, _blackBoard.swingPoint.position);
            _blackBoard.lr.SetPosition(1, _blackBoard.playerSwingPoint.position);
            
        }
    }
}