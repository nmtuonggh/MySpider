using UnityEngine;

namespace SFRemastered._Game._Scripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private RectTransform suitUI;

        #region SuitUI

        public void ShowSuitUI()
        {
            suitUI.gameObject.SetActive(true);
        }
        
        public void HideSuitUI()
        {
            suitUI.gameObject.SetActive(false);
        }

        #endregion
        
        
    }
}