using System;
using UnityEngine;

namespace SFRemastered.SwingHolder
{
    public class SwingHolderController : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private Vector3 _offset;

        private void Update()
        {
            Vector3 position = _player.transform.position + _player.transform.forward*10f + _offset;
            transform.position = position;
            float yRotation = _player.transform.rotation.eulerAngles.y;
            
            // Set the SwingHolder's rotation with only the y-axis from the player
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}