using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Player.State.Combat.Gadget
{
    public class ProjectilePool : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private int poolSize = 10;
        private Queue<GameObject> pool;

        private void Awake()
        {
            pool = new Queue<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                GameObject projectile = Instantiate(projectilePrefab);
                projectile.SetActive(false);
                pool.Enqueue(projectile);
            }
        }

        public GameObject GetProjectile()
        {
            if (pool.Count > 0)
            {
                GameObject projectile = pool.Dequeue();
                projectile.SetActive(true);
                return projectile;
            }
            else
            {
                GameObject projectile = Instantiate(projectilePrefab);
                return projectile;
            }
        }

        public void ReturnProjectile(GameObject projectile)
        {
            projectile.SetActive(false);
            pool.Enqueue(projectile);
        }
    }
}