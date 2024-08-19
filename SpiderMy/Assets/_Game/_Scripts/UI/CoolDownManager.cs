using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Player.State.Combat.Gadget
{
    public class CoolDownManager : MonoBehaviour
    {
        public List<GadgetBase> allSkills;

        private void Update()
        {
            foreach (var skill in allSkills)
            {
                if (skill.currentCoolDown > 0)
                {
                    skill.currentCoolDown -= Time.deltaTime;
                    if (skill.currentCoolDown <= 0)
                    {
                        skill.ReplenishStack();
                    }
                }
            }
        }
    }
}