using _Game.Scripts.Event;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission.Collectible
{
    public class ScanBtn : MonoBehaviour
    {
        public GameEventListener _onHaveChestInRange;
        public GameEventListener _onNoChestInRange;
        public ParticleSystem _particleSystem;
        
        private void OnEnable()
        {
            _onHaveChestInRange.OnEnable();
            _onNoChestInRange.OnEnable();
        }
        
        private void OnDisable()
        {
            _onHaveChestInRange.OnDisable();
            _onNoChestInRange.OnDisable();
        }
        
        public void HaveChestInRange()
        {
            _particleSystem.Play();
        }
        
        public void NoChestInRange()
        {
            _particleSystem.Stop();
        }
        
    }
}