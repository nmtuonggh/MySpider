using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered
{
    public class SuitManager : MonoBehaviour
    {
        [SerializeField] private BlackBoard blackBoard;
        [SerializeField] private List<SuitData> suitList;

        public void ChangeSuit(int ID)
        {
            /*foreach (var suits in suitList)
            {
                suits.gameObject.SetActive(false);
            }*/

            for (int i = 0; i < suitList.Count; i++)
            {
                var suit = suitList[i];
                if (suit.suitID == ID)
                {
                    suit.gameObject.SetActive(true);
                    //suit.ApplySkin(blackBoard);
                    blackBoard.SetSuitData(suit);
                }
                else
                {
                    suit.gameObject.SetActive(false);
                }
            }
            
        }
    }
}