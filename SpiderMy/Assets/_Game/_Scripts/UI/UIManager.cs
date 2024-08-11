using System;
using System.Collections.Generic;
using SFRemastered._Game._Scripts.Player.State.Combat.Gadget;
using UnityEngine;

namespace SFRemastered._Game._Scripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private RectTransform suitUI;
        [SerializeField] private BlackBoard blackBoard;

        [Header("============Gadget============")]
        [SerializeField] private List<GameObject> gadgetsIconBtn;

        private void Awake()
        {
        }

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

        #region Gadget

        public void SetGadgetUI()
        {
            foreach (var icon in gadgetsIconBtn)
            {
                icon.SetActive(false);
            }

            if (blackBoard.gadgetIndex > -1)
            {
                gadgetsIconBtn[blackBoard.gadgetIndex].gameObject.SetActive(true);
            }
        }

        #endregion
        
    }
}