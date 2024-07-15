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
            transform.position = _player.transform.position + _offset;
            var transformRotation = transform.rotation;
            transformRotation.y = _player.transform.rotation.y;
        }
    }
}