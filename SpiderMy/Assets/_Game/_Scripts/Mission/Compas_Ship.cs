using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    public class Compas_Ship : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}