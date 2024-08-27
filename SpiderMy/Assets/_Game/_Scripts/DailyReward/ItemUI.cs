using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SFRemastered._Game._Scripts.DailyReward
{
    public class ItemUI : MonoBehaviour
    {
        public Image image;
        public TextMeshProUGUI amount;
        public GameObject checkMark;
        public GameObject focus;
        
        public void SetData(Sprite imageData, string amount)
        {
            image.sprite = imageData;
            this.amount.text = amount;
        }
        
        public void SetFocus(bool isFocus)
        {
            focus.SetActive(isFocus);
        }
        
        public void SetCheckMark(bool isCheck)
        {
            checkMark.SetActive(isCheck);
        }
    }
}