using UnityEngine;

namespace SFRemastered.SwingHolder
{
    public class SwingPoint : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField] private Vector3 _offset;

        private void Update()
        {
            Vector3 position = root.transform.position +  root.transform.forward * 30 + _offset;
            transform.position = position;
            float yRotation = root.transform.rotation.eulerAngles.y;
            
            // Set the SwingHolder's rotation with only the y-axis from the player
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}