using System;
using SFRemastered._Game._Scripts.Data;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Player.State.Combat.Gadget
{
    public class HealingBotController : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform startPosition;
        [SerializeField] private float rotationSpeed = 50f;
        [SerializeField] private float radius = 2f;
        [SerializeField] private float angle = 0f;
        [SerializeField] private float smoothSpeed = 0.125f;
        public float elapsedTime;
        [SerializeField] private float duration = 10f;
        [SerializeField] private HealingBotSO healingBotSO;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private PlayerDataSO playerData;
        [SerializeField] private PoolObject healVFX;

        private GameObject healEffect;
        private Vector3 currentOffset;
        private bool healEffectSpawned = false;

        private void Start()
        {
            
        }

        private void OnEnable()
        {
            elapsedTime = 0;
            healEffectSpawned = false;
            /*currentOffset = new Vector3(Mathf.Cos(angle) * radius, 2, Mathf.Sin(angle) * radius);
            transform.position = startPosition.position;*/
            currentOffset = startPosition.position - playerTransform.position;
            transform.position = startPosition.position;
        }

        private void LateUpdate()
        {
            if (playerTransform == null) return;
            
            
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= duration)
            {
                gameObject.SetActive(false);
                elapsedTime = 0;
                healVFX.ReturnToPool(healEffect);
                return;
            }

            angle += rotationSpeed * Time.deltaTime;
            Vector3 targetOffset = new Vector3(Mathf.Cos(angle) * radius, 2, Mathf.Sin(angle) * radius);
            
            currentOffset = Vector3.Lerp(currentOffset, targetOffset, smoothSpeed);
            transform.position = playerTransform.position + currentOffset;
            transform.LookAt(playerTransform);
        }

        private void Update()
        {
            if (elapsedTime < duration)
            {
                float healAmount = 10f * Time.deltaTime;
                playerController.health = Mathf.Min(playerController.health + healAmount, playerData.maxHealth);
                playerData.currentHealth = playerController.health;
                
                if (!healEffectSpawned)
                {
                    healEffect = healVFX.Spawn(playerTransform.transform.position, Quaternion.identity, playerTransform);
                    healEffectSpawned = true;
                }
            }
        }
    }
    
}