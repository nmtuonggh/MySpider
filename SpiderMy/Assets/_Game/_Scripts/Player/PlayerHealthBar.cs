using SFRemastered._Game._Scripts.Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace SFRemastered
{
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField] private Slider healthBar;
        [SerializeField] private Slider easeHealthBar;
        
        [SerializeField] private float lerpSpeed;
        [SerializeField] private PlayerController playerController;
        
        private void Start()
        {
            gameObject.SetActive(true);
            healthBar.maxValue = playerController.health;
            easeHealthBar.maxValue = playerController.health;
        }

        private void Update()
        {
            if (healthBar.value > playerController.health)
            {
                healthBar.value = playerController.health;
            }
            
            if (healthBar.value < playerController.health)
            {
                healthBar.value = playerController.health;
                healthBar.value = Mathf.Lerp(healthBar.value, playerController.health, lerpSpeed * Time.deltaTime);
            }
            
            if (healthBar.value != easeHealthBar.value)
            {
                easeHealthBar.value = Mathf.Lerp(easeHealthBar.value, playerController.health, lerpSpeed * Time.deltaTime);
            }
        }
        
        public void TakeDamage(float damage)
        {
            healthBar.value -= damage;
            
            if (healthBar.value <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}