using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Gadget
{
    public class GadgetUI : MonoBehaviour
    {
        public List<GameObject> gadgetsUI;
        
        public void SetGadgetUI(int index)
        {
            foreach (var gadgetUI in gadgetsUI)
            {
                gadgetUI.SetActive(false);
            }

            if (index > -1)
            {
                gadgetsUI[index].SetActive(true);
            }
        }
    }
}