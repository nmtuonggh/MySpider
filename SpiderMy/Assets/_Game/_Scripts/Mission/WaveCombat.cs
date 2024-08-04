using System.Collections.Generic;
using SFRemastered._Game._Scripts.Enemy;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    [System.Serializable]
    public class WaveCombat
    {
        
        public List<GangsterSO> listGangster = new List<GangsterSO>();
        public List<Mercenary> listMercenary = new List<Mercenary>();
        public List<Mafia> listMafia = new List<Mafia>();
        
    }
}