using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered
{
    public class SuitManager : MonoBehaviour
    {
        [SerializeField] private BlackBoard blackBoard;
        [SerializeField] private SuitData currentSuitData;
        
        [SerializeField] private List<SuitData> suitList;
        [SerializeField] private int currentSkinIndex = 0;
        [SerializeField] private int previousSkinIndex = 0;

        public void ChangeSuit()
        {
            suitList[currentSkinIndex].gameObject.SetActive(false);
            previousSkinIndex = currentSkinIndex;
            
            if (currentSkinIndex == suitList.Count - 1)
            {
                currentSkinIndex = 0;
            }
            else
            {
                currentSkinIndex++;
            }

            currentSuitData = suitList[currentSkinIndex];
            currentSuitData.gameObject.SetActive(true);
            
            blackBoard.SetSuitData(currentSuitData);
        }
    }
}