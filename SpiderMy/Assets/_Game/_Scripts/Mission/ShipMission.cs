using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Event;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SFRemastered._Game._Scripts.Mission
{
    public class ShipMission : BaseMission
    {
        
        [Header("Ship Mission Variables")]
        public ShippingMissionSO shippingMissionSO;
        private GameObject _indicator;
        private GameObject _missionRange;
        [SerializeField] private int deliverySuccess;
        private List<GameObject> _deliveryGameObjects = new List<GameObject>();
        private List<GameObject> _deliveryNPCGameObjects = new List<GameObject>();
        
        [Header("NPC Prefabs")]
        public GameObject pickupNPCPrefab;
        public GameObject deliveryNPCPrefab;
        public GameObject deliveryIndicatorPrefab;
        
        [Header("Game Event")]
        public GameEventListener onInPickup;
        public GameEventListener onOutPickup;
        public GameEventListener onInDelivery;
        public GameEventListener onOutDelivery;
        public GameEventListener onOutOfTime;
        
        private void OnEnable()
        {
            onInPickup.OnEnable();
            //onOutPickup.OnEnable();
            onInDelivery.OnEnable();
            onOutOfTime.OnEnable();
            //onOutDelivery.OnEnable();
        }
        
        private void OnDisable()
        {
            onInPickup.OnDisable();
            //onOutPickup.OnDisable();
            onInDelivery.OnDisable();
            onOutOfTime.OnDisable();
            //onOutDelivery.OnDisable();
        }
        

        public override void StartMission()
        {
            base.StartMission();
            deliverySuccess = 0;
            SpawnPickupLocation();
            DrawnIndicator();
        }
        
        private void DrawnIndicator()
        {
            _indicator = shippingMissionSO.indicatorPrefab.Spawn(shippingMissionSO.SpawnPosition.position,
                Quaternion.identity, shippingMissionSO.SpawnPosition);
        }

        private void SpawnPickupLocation()
        {
           _missionRange = shippingMissionSO.missionRangePrefab.Spawn(shippingMissionSO.SpawnPosition.position,
               Quaternion.identity, shippingMissionSO.SpawnPosition);
        }

        public void InPickup()
        {
            _deliveryNPCGameObjects.Clear();
            _deliveryGameObjects.Clear();
            foreach (Vector3 shipPoint in shippingMissionSO.listDeliveryPoints)
            {
                var a = shippingMissionSO.deliveryNPCPrefab.Spawn(shipPoint + new Vector3(0,1,0),
                    Quaternion.identity, shippingMissionSO.SpawnPosition);
                var b = shippingMissionSO.deliveryPointPrefab.Spawn(shipPoint + new Vector3(0,1,0),
                    Quaternion.identity, shippingMissionSO.SpawnPosition);
                _deliveryNPCGameObjects.Add(a);
                _deliveryGameObjects.Add(b);
            }
            shippingMissionSO.missionRangePrefab.ReturnToPool(_missionRange);
            shippingMissionSO.indicatorPrefab.ReturnToPool(_indicator);
        }

        
        
        public void OnInDelivery()
        {
            if (deliverySuccess < shippingMissionSO.listDeliveryPoints.Count)
            {
                deliverySuccess++;
            }
            
            if (deliverySuccess == shippingMissionSO.listDeliveryPoints.Count)
            {
                CompleteMission();
            }
        }
        
        

        public override void UpdateMission()
        {
            base.UpdateMission();
        }
        
        public override void CompleteMission()
        {
            base.CompleteMission();
        }
        
        public override void FailMission()
        {
            base.FailMission();
            shippingMissionSO.missionRangePrefab.ReturnToPool(_missionRange);
            shippingMissionSO.indicatorPrefab.ReturnToPool(_indicator);
            foreach (var var in _deliveryGameObjects)
            {
                if (var!=null)
                {
                    shippingMissionSO.deliveryPointPrefab.ReturnToPool(var);
                }
            }
            foreach (var var in _deliveryNPCGameObjects)
            {
                if (var!=null)
                {
                    shippingMissionSO.deliveryNPCPrefab.ReturnToPool(var);
                }
            }
        }
    }
}