﻿using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered._Game._Scripts.FSM.Swing.Test
{
    public class Swing : MonoBehaviour
    {
        [SerializeField]
        public ConLac conLac;

        private void Start()
        {
            conLac.Initialise();
        }

        private void FixedUpdate()
        {
            transform.localPosition = conLac.MoveSpider(transform.localPosition, Time.fixedDeltaTime);
        }
    }
}
