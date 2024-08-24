using System;
using _Game.Scripts.Event;
using DamageNumbersPro;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Player.State.Combat
{
    public class ComboCounter : MonoBehaviour
    {
        public GameEventListener addHit;
        
        [SerializeField] private int _comboCounter = 0;
        [SerializeField] private float timeToResetCombo;
        [SerializeField] private GameObject pos;
        public DamageNumber damageNumber;
        //[SerializeField] private GameObject p;
        private float currentTime = 0;

        
        private void OnEnable()
        {
            addHit.OnEnable();
            damageNumber.cameraOverride = Camera.main.transform;
        }
        
        private void OnDisable()
        {
            addHit.OnDisable();
        }
        private void Update()
        {
            if (_comboCounter > 0)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= timeToResetCombo)
                {
                    _comboCounter = 0;
                    currentTime = 0;
                }
            }
        }
        
        public void AddHit()
        {
            _comboCounter++;
            damageNumber.Spawn(pos.transform.position, _comboCounter);
            currentTime = 0;
        }
    }
}