using UnityEngine;

namespace SFRemastered._Game._Scripts.Enemy
{
    public interface IHitable
    {
         void OnStaggerHit(float damage)
        {
            
        }
        
         void OnKnockBackHit(float damage)
        {
            
        }
    }
}