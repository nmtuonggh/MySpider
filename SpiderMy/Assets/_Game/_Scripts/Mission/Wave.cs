using System.Collections.Generic;
using SFRemastered._Game._Scripts.Enemy;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Wave")]
    public class Wave : ScriptableObject
    {
        public float enemyCount;
        
        public List<GangsterSO> listGangster;
        public List<Mercenary> listMercenary;
        public List<Mafia> listMafia;
    }
}