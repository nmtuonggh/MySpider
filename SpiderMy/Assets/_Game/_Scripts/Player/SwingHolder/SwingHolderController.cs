using System;
using UnityEngine;

namespace SFRemastered.SwingHolder
{
    public class SwingHolderController : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField] private Vector3 _offset;

        private void Update()
        {
            Vector3 forwardXZ = new Vector3(root.transform.forward.x, 0, root.transform.forward.z).normalized;

            // Calculate the position 30 units in front of the root on the x-z plane and 20 units above the root
            Vector3 position = root.transform.position + forwardXZ * 35f + Vector3.up * 25f;
            transform.position = position;
        }
    }
}