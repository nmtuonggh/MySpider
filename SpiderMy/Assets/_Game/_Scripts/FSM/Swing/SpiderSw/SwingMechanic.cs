using UnityEngine;

namespace SFRemastered._Game._Scripts.FSM.Swing.SpiderSw
{
    [System.Serializable]
    public class SwingMechanic
    {
        public RopeLenght ropeLenght;
        public TargetSwing targetSwing;
        public Vector3 previousPositon;
        public PlayerSwing playerSwing;

        public void Initialise()
        {
            ropeLenght.lenght = Vector3.Distance(targetSwing.targetPos, playerSwing.transform.position);
        }

        public Vector3 SwingMove(Vector3 positon, float time)
        {
            playerSwing.velocity += GetConstrainedVelocity(positon, previousPositon, time);
            playerSwing.ApplyGravity();
            playerSwing.ApplyDamping();
            playerSwing.CapMaxSpeed();

            positon += playerSwing.velocity * time;

            if (Vector3.Distance(positon, targetSwing.targetPos) < ropeLenght.lenght)
            {
                positon = Vector3.Normalize(positon - targetSwing.targetPos) * ropeLenght.lenght;
                ropeLenght.lenght = Vector3.Distance(positon, targetSwing.targetPos);
                return positon;
            }

            previousPositon = positon;
            return positon;
        }
        
        public Vector3 SwingMove(Vector3 positon, Vector3 prePos, float time)
        {
            playerSwing.velocity += GetConstrainedVelocity(positon, prePos, time);
            playerSwing.ApplyGravity();
            playerSwing.ApplyDamping();
            playerSwing.CapMaxSpeed();

            positon += playerSwing.velocity * time;

            if (Vector3.Distance(positon, targetSwing.targetPos) < ropeLenght.lenght)
            {
                positon = Vector3.Normalize(positon - targetSwing.targetPos) * ropeLenght.lenght;
                ropeLenght.lenght = Vector3.Distance(positon, targetSwing.targetPos);
                return positon;
            }

            previousPositon = positon;
            return positon;
        }

        public Vector3 GetConstrainedVelocity(Vector3 currentPos, Vector3 previousPos, float time)
        {
            Vector3 constrainedPosition;
            Vector3 predictedPosition;
            float distanceToTarget = Vector3.Distance(currentPos, targetSwing.targetPos);
            if (distanceToTarget > ropeLenght.lenght)
            {
                constrainedPosition = Vector3.Normalize(currentPos - targetSwing.targetPos) * ropeLenght.lenght;
                predictedPosition = (constrainedPosition - previousPos) / time;
                return predictedPosition;
            }

            return Vector3.zero;
        }
    }
}