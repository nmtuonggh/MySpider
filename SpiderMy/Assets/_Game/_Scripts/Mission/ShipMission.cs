﻿using System;
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
        
        [Header("NPC Prefabs")]
        public GameObject pickupNPCPrefab;
        public GameObject deliveryNPCPrefab;
        public GameObject deliveryIndicatorPrefab;
        
        [Header("Game Event")]
        public GameEventListener onInPickup;
        public GameEventListener onOutPickup;
        public GameEventListener onInDelivery;
        public GameEventListener onOutDelivery;
        
        private void OnEnable()
        {
            onInPickup.OnEnable();
            //onOutPickup.OnEnable();
            onInDelivery.OnEnable();
            //onOutDelivery.OnEnable();
        }
        
        private void OnDisable()
        {
            onInPickup.OnDisable();
            //onOutPickup.OnDisable();
            onInDelivery.OnDisable();
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
            _indicator = Instantiate(shippingMissionSO.indicatorPrefab, shippingMissionSO.SpawnPosition.position,
                Quaternion.identity);
            _indicator.transform.SetParent(shippingMissionSO.SpawnPosition);
        }

        private void SpawnPickupLocation()
        {
           _missionRange = Instantiate(shippingMissionSO.missionRangePrefab, shippingMissionSO.SpawnPosition.position,
               Quaternion.identity);
           _missionRange.transform.SetParent(shippingMissionSO.SpawnPosition);
        }

        public void InPickup()
        {
            foreach (Vector3 shipPoint in shippingMissionSO.listDeliveryPoints)
            {
                Instantiate(deliveryNPCPrefab, shipPoint + Vector3.up*2f, Quaternion.identity, shippingMissionSO.SpawnPosition);
                Instantiate(shippingMissionSO.deliveryPointPrefab, shipPoint, Quaternion.identity, shippingMissionSO.SpawnPosition);
            }

            Destroy(_missionRange);
            Destroy(_indicator);
        }

        
        
        public void OnInDelivery()
        {
            if (deliverySuccess < shippingMissionSO.listDeliveryPoints.Count)
            {
                deliverySuccess++;
            }
            
            if (deliverySuccess == shippingMissionSO.listDeliveryPoints.Count)
            {
                Debug.Log("All Delivery Success");
                CompleteMission();
            }
            
            //Destroy all the child of 
            Debug.Log("Delivery Success: " + deliverySuccess);
            
            //TODO: Show UI for delivery success
        }
        
        

        public override void UpdateMission()
        {
            base.UpdateMission();
        }
        
        public override void CompleteMission()
        {
            Debug.Log("Mission Complete");
            base.CompleteMission();
        }
        
        public override void FailMission()
        {
            base.FailMission();
        }
    }
}