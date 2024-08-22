using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered._Game._Scripts.Mission
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Mission/ShippingMission")]

    public class ShippingMissionSO : BaseMissionSO
    { 
        [Header("Ship Mission")]
        public List<Vector3> listDeliveryPoints = new List<Vector3>();
        public GameObject deliveryPointPrefab;
        public override void GetMissionPosition(Transform mssPoint)
        {
            base.GetMissionPosition(mssPoint);
            listDeliveryPoints.Clear();
            
            foreach (Transform child in mssPoint)
            {
                listDeliveryPoints.Add(child.position);
            }
        }
        
    }
}