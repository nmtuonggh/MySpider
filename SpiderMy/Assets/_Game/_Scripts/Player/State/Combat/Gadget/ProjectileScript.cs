using System;
using SFRemastered._Game._Scripts.Enemy;
using SFRemastered._Game._Scripts.Enemy.State;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Player.State.Combat.Gadget
{
    public class ProjectileScript : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private ProjectileData projectileData;
        private void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyBlackBoard>().webHitStun += 1;
                other.GetComponent<EnemyBlackBoard>().stunLockHit = true;
                projectileData.ReturnToPool(this.gameObject);
            }
        }
    }
}