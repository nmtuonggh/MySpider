using System.Collections.Generic;
using SFRemastered._Game._Scripts.Player.State.Combat.Gadget;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered._Game._Scripts.Mission
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Mission/ShippingMission")]

    public class ShippingMissionSO : BaseMissionSO
    { 
        [Header("Ship Mission")]
        public List<Vector3> listDeliveryPoints = new List<Vector3>();
        public PoolObject deliveryPointPrefab;
        public PoolObject deliveryNPCPrefab;
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