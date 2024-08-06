using UnityEngine;

namespace SFRemastered._Game._Scripts.SuitUI
{
    [CreateAssetMenu(menuName = "ScriptableObjects/SuitUI/SkinItemData")]
    public class SkinItemDataSO : ScriptableObject
    {
        [Header("Suit Information")]
        public int suitID;
        public string suitName;
        public Sprite suitImage;
        public Sprite background;
        
        [Header("Suit Data")]
        public bool isActivated;
        public int cardRequired;
        public int cardOwned;
        
        [Header("Suit Stats")]
        public int health;
        public int damage;
    }
}