using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    public class UI_ShowMission : MonoBehaviour
    {
        public void ShowMissionUI(GameObject ui, bool status)
        {
            ui.SetActive(status);
        }
        
        
    }
}