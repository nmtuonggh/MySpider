using System;
using SFRemastered._Game._Scripts.Player.State.Combat.Gadget;
using SFRemastered.InputSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SFRemastered._Game._Scripts.Gadget
{
    public class GadgetBtnSkill : MonoBehaviour
    {
        public Button skillBtn;
        public TextMeshProUGUI cooldownText;
        public TextMeshProUGUI stackText;
        public GadgetBase gadgetData;
        public InputActionButton inputActionButton;
        public CoolDownManager coolDownManager;

        public GameObject bgDisable;
        public GameObject bgEnable;
        public GameObject bgCooldown; 

        private void Start()
        {
            UpdateUI();
            coolDownManager.allSkills.Add(gadgetData);
        }

        private void Update()
        {
            bgDisable.SetActive(gadgetData.currentStack == 0);
            bgEnable.SetActive(gadgetData.currentStack > 0);
            bgCooldown.SetActive(cooldownText.IsActive());

            if (gadgetData.currentCoolDown > 0 && gadgetData.currentStack < gadgetData.maxStack)
            {
                cooldownText.gameObject.SetActive(true);
                cooldownText.text = gadgetData.currentCoolDown.ToString("F0");
                UpdateUI();
            }
            else if (cooldownText.gameObject.activeSelf)
            {
                cooldownText.gameObject.SetActive(false);
                UpdateUI();
            }
        }

        public void UpdateUI()
        {
            if (gadgetData.currentStack > 1)
            {
                stackText.text = gadgetData.currentStack.ToString();
            }
            else
            {
                stackText.text = "";
            }
            skillBtn.interactable = gadgetData.currentStack > 0;
        }
    }
}