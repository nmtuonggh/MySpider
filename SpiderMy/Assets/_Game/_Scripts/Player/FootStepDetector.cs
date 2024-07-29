using System.Collections;
using System.Collections.Generic;
using NodeCanvas.Framework;
using UnityEngine;

namespace SFRemastered
{
    public class FootStepDetector : MonoBehaviour
    {
        [SerializeField] private BlackBoard blackBoard;
        private void OnTriggerEnter(Collider other)
        {
            blackBoard.sfxManager.PlayFootstep();
        }
    }
}
