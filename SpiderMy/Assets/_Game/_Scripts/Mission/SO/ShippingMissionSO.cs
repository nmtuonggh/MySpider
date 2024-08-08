using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered._Game._Scripts.Mission
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Mission/ShippingMission")]

    public class ShippingMissionSO : BaseMissionSO
    { 
        [Header("Ship Mission")]
        public List<Transform> listDeliveryPoints = new List<Transform>();
        public GameObject deliveryPointPrefab;

        public override void GetMissionPosition(Transform mssPoint)
        {
            base.GetMissionPosition(mssPoint);
            foreach (Transform transform in mssPoint)
            {
                listDeliveryPoints.Add(transform);
            }
        }
    }
}