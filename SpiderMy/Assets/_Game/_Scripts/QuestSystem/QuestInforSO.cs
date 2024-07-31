using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered
{
    [CreateAssetMenu(fileName = "QuestInforSO", menuName = "ScriptableObjects/QuestInforSO")]
    public class QuestInforSO : ScriptableObject
    {
        [field: SerializeField] public string id { get; private set; }

        [Header("General Information")] public string displayName;

        //[Header("Requirements")] public QuestInforSO[] questPreRequisites;

        [Header("Steps")] public GameObject[] questStepPrefabs;

        [Header("Rewards")] public int cashReward;
        public int expReward;

        private void OnValidate()
        {
#if UNITY_EDITOR
            id = this.name;
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        
    }
}