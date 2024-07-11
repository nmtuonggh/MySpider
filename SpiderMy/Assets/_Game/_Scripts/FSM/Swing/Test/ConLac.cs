using SFRemastered._Game._Scripts.FSM.Swing;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
    [System.Serializable]
    public class ConLac
    {
        public Transform spider_tr;
        public Arm arm;
        public Tether tether;
        public Spider spider;
        public Vector3 previousPositon;

        public void Initialise()
        {
            //spider_tr.transform.parent = tether.tetherTransform;
            arm.length= Vector3.Distance(spider_tr.transform.position, tether.tetherTransform.position);
        }
        
        public Vector3 MoveSpider(Vector3 pos, float time)
        {
            spider.velocity += GetConstrainedVelocity(pos, previousPositon, time);
            spider.ApplyGravity();
            spider.ApplyDamping();
            spider.CapMaxSpeed();
            
            pos += spider.velocity * time;

            if (Vector3.Distance(pos, tether.point) < arm.length)
            {
                pos = Vector3.Normalize(pos - tether.point) * arm.length;
                arm.length = Vector3.Distance(pos, tether.point);
                return pos;
            }

            previousPositon = pos;
            return pos;
        }
        
        public Vector3 MoveSpider(Vector3 pos, Vector3 prePos, float time)
        {
            spider.velocity += GetConstrainedVelocity(pos, prePos, time);
            spider.ApplyGravity();
            spider.ApplyDamping();
            spider.CapMaxSpeed();
            
            pos += spider.velocity * time;

            if (Vector3.Distance(pos, tether.point) < arm.length)
            {
                pos = Vector3.Normalize(pos - tether.point) * arm.length;
                arm.length = Vector3.Distance(pos, tether.point);
                return pos;
            }

            previousPositon = pos;
            return pos;
        }
        
        public Vector3 GetConstrainedVelocity(Vector3 currentPos, Vector3 previousPos, float time)
        {
            float distanceToTether;
            Vector3 constrainedPosition;
            Vector3 predictedPosition;
            
            distanceToTether = Vector3.Distance(currentPos, tether.point);
            if(distanceToTether > arm.length)
            {
                constrainedPosition = Vector3.Normalize(currentPos - tether.point) * arm.length ;
                predictedPosition = (constrainedPosition - previousPos) / time;
                return predictedPosition;
            }
            return Vector3.zero;    
        }

        public void SwitchTether(Vector3 newPosition)
        {
            Debug.Log("run");
            spider_tr.transform.parent = null;
            tether.tetherTransform.position = newPosition;
            Debug.Log(tether.tetherTransform.name);
            spider_tr.transform.parent = tether.tetherTransform;
            tether.point = tether.tetherTransform.InverseTransformPoint(newPosition);
            arm.length = Vector3.Distance(spider_tr.transform.localPosition, tether.point);
        }

        public Vector3 Fall(Vector3 pos, float time)
        {
            spider.ApplyGravity();
            spider.ApplyDamping();
            spider.CapMaxSpeed();
            
            pos += spider.velocity * time;
            return pos;
        }
    }
