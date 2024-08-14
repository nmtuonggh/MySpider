using System;
using UnityEngine;
using UnityEngine.UI;

namespace SFRemastered._Game._Scripts.Enemy
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] public Slider healthBar;
        [SerializeField] public Slider easeHealthBar;
        
        [SerializeField] private float lerpSpeed;
        
        [SerializeField] private EnemyController enemyController;
        
        private void OnEnable()
        {
            /*gameObject.SetActive(true);
            healthBar.maxValue = enemyController.health;
            easeHealthBar.maxValue = enemyController.health;*/
        }

        private void Update()
        {
            if (healthBar.value != enemyController.health)
            {
                healthBar.value = enemyController.health;
            }
            
            if (healthBar.value != easeHealthBar.value)
            {
                easeHealthBar.value = Mathf.Lerp(easeHealthBar.value, enemyController.health, lerpSpeed * Time.deltaTime);
            }

            if (Camera.main != null) transform.LookAt(transform.position + Camera.main.transform.forward);
        }
        
        public void TakeDamage(float damage)
        {
            healthBar.value -= damage;
            
            /*if (healthBar.value <= 0)
            {
                gameObject.SetActive(false);
            }*/
        }
    }
}