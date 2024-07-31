using UnityEngine;

namespace SFRemastered._Game._Scripts.Enemy
{
    public class EnemySO : ScriptableObject
    {
        public GameObject prefab;
        
        public float health;
        public float damage;
        public float speed;
        
        public float attackRange;
        public float attackCooldown;
        
    }
}