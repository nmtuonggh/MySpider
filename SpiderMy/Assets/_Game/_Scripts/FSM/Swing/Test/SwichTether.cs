using System;
using UnityEngine;

namespace SFRemastered._Game._Scripts.FSM.Swing.Test
{
    public class SwichTether : MonoBehaviour
    {
        public Transform newTether;
        public Swing swing;

        private void Update()
        {
            if (Input.GetKey(KeyCode.F))
            {
                swing.conLac.SwitchTether(newTether.transform.position);
            }
        }
    }
}