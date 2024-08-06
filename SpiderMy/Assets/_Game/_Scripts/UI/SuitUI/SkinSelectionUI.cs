using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered._Game._Scripts.SuitUI
{
    public class SkinSelectionUI : MonoBehaviour
    {
        [SerializeField] private GameObject skinItemPrefab;
        [SerializeField] private Transform skinItemContainer;
        [SerializeField] private List<SkinItemDataSO> skinItemDataList;
        [SerializeField] private SuitManager suitManager;
        

        private void Start()
        {
            PopulateSkinItems();
        }

        private void PopulateSkinItems()
        {
            for (int i = 0; i < skinItemDataList.Count; i++)
            {
                var skinItemData = skinItemDataList[i];
                var skinItemObject = Instantiate(skinItemPrefab, skinItemContainer);
                var skinItemUI = skinItemObject.GetComponent<SkinItemUI>();
                skinItemUI.Initialize(skinItemData, i, suitManager);
            }
        }
    }
}