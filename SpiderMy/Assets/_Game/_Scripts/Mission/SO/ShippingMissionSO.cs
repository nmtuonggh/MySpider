using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered._Game._Scripts.Mission
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Mission/ShippingMission")]

    public class ShippingMissionSO : BaseMissionSO
    { 
        [Header("Ship Mission")]
        public List<Transform> listDeliveryPoints;
        public GameObject deliveryPointPrefab;
    }
}