using UnityEngine;

namespace SFRemastered._Game._Scripts.Player.State.Combat.Gadget
{
    public class HealingBotController : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float rotationSpeed = 50f;
        [SerializeField] private float radius = 2f;
        [SerializeField] private float angle = 0f;
        [SerializeField] private float smoothSpeed = 0.125f;
        private float elapsedTime;
        [SerializeField] private float duration = 10f;
        [SerializeField] private HealingBotSO healingBotSO;

        private Vector3 currentOffset;

        private void Start()
        {
            elapsedTime = 0;
            currentOffset = new Vector3(Mathf.Cos(angle) * radius, 2, Mathf.Sin(angle) * radius);
        }

        private void LateUpdate()
        {
            if (playerTransform == null) return;
            
            
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= duration)
            {
                healingBotSO.ReturnToPool(gameObject);
                return;
            }

            angle += rotationSpeed * Time.deltaTime;
            Vector3 targetOffset = new Vector3(Mathf.Cos(angle) * radius, 2, Mathf.Sin(angle) * radius);
            
            currentOffset = Vector3.Lerp(currentOffset, targetOffset, smoothSpeed);
            transform.position = playerTransform.position + currentOffset;
            transform.LookAt(playerTransform);
        }
    }
    
}