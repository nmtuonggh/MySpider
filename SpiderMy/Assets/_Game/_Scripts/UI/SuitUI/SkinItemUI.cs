using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SFRemastered._Game._Scripts.SuitUI
{
    public class SkinItemUI : MonoBehaviour
    {
        public int suitID;
        public Image skinImage;
        public Image background;

        public TextMeshProUGUI cardData;
        public Image blockUI;
        public Image activeUI;
        public Image cardUI;
        public Image Focus;
        
        private SuitManager suitManager;
        private bool isActivated;

        public void Initialize(SkinItemDataSO skinItemDataSo, int id, SuitManager manager)
        {
            suitID = skinItemDataSo.suitID;
            skinImage.sprite = skinItemDataSo.suitImage;
            cardData.text = skinItemDataSo.cardOwned.ToString() + "/" + skinItemDataSo.cardRequired.ToString();
            background.sprite = skinItemDataSo.background;
            suitManager = manager;

            // Check if cardOwned is greater than or equal to cardRequired
            if (skinItemDataSo.cardOwned >= skinItemDataSo.cardRequired)
            {
                isActivated = true;
                activeUI.gameObject.SetActive(true);
                blockUI.gameObject.SetActive(false);
                cardUI.gameObject.SetActive(false);
            }
            else
            {
                isActivated = false;
                activeUI.gameObject.SetActive(false);
                blockUI.gameObject.SetActive(true);
                cardUI.gameObject.SetActive(true);
            }
        }

        public void OnClick()
        {
            if (isActivated)
            {
                suitManager.ChangeSuit(suitID);
            }
        }
    }
}