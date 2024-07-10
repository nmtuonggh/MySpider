using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered.Swing
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/StartSwing")]
    public class StartSwing: SwingState
    {
        
        [FormerlySerializedAs("velocity")] public Vector3 lenght;
        public override void EnterState()
        {
            base.EnterState();
            lenght = _blackBoard.swingPoint.position - _blackBoard.playerSwingPoint.position;
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            DrawLine();
            HandelForce();

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
            _blackBoard.lr.positionCount = 0;
            _blackBoard.swing = false;
        }

        public void HandelForce()
        {
            var v = _blackBoard.rigidbody.velocity.magnitude;
            //var v = 10f;
            //var r = 10;
            var r = _blackBoard.swingPoint.position - _blackBoard.playerSwingPoint.position;
            var gravity = _blackBoard.playerMovement.GetGravityVector();
            
            //Tension
            var tensionDirection = _blackBoard.swingPoint.position - _blackBoard.playerSwingPoint.position;
            //currentAlpha = Vector3.Angle(Vector3.down, r);
            var tensionMagnitude = v * v / r.magnitude;
            //gravity
            var gPara = Vector3.Project(gravity, tensionDirection);
            var gPerpMag = gravity.magnitude * Mathf.Sin(Vector3.Angle(gravity, _blackBoard.swingPoint.position - _blackBoard.playerSwingPoint.position));
            var gPerp = gravity - gPara;

            Vector3 totalForce = tensionDirection  + gPerp;

            _blackBoard.rigidbody.AddForce(totalForce);
            Debug.Log("velocity: " + v + " r: " + r + " totalForce: " + totalForce.magnitude);
            
            //Debug.DrawRay(_blackBoard.playerSwingPoint.position, gravity, Color.red);
            Debug.DrawRay(_blackBoard.playerSwingPoint.position, gPerp, Color.green);
            Debug.DrawRay(_blackBoard.playerSwingPoint.position, tensionDirection * tensionMagnitude, Color.yellow);
            Debug.DrawRay(_blackBoard.playerSwingPoint.position, totalForce, Color.cyan);
        }
        
        public void DrawLine()
        {
            _blackBoard.lr.positionCount = 2;
            _blackBoard.lr.SetPosition(0, _blackBoard.swingPoint.position);
            _blackBoard.lr.SetPosition(1, _blackBoard.playerSwingPoint.position);
        }
    }
}