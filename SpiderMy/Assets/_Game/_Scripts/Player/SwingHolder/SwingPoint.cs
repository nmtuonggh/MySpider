using UnityEngine;

namespace SFRemastered.SwingHolder
{
    public class SwingPoint : MonoBehaviour
    {
        [SerializeField] private GameObject root;

        private void Update()
        {
            Vector3 forwardXZ = new Vector3(root.transform.forward.x, 0, root.transform.forward.z).normalized;

            // Calculate the position 30 units in front of the root on the x-z plane and 20 units above the root
            Vector3 position = root.transform.position + forwardXZ * 35f + Vector3.up * 20f;
            transform.position = position;
            
        }
    }
}