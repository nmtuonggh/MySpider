using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered
{
    public abstract class QuestStep : MonoBehaviour
    {
        private bool isFinished = false;
        
        protected void FinishQuestStep()
        {
            isFinished = true;
            
            //TODO - advance to next step
            
            Destroy(this.gameObject);
        }
    }
}
