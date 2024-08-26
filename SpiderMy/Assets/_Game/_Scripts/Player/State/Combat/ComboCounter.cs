using System;
using _Game.Scripts.Event;
using DamageNumbersPro;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Player.State.Combat
{
    public class ComboCounter : MonoBehaviour
    {
        public GameEventListener addHit;
        
        [SerializeField] private int _comboCounter = 0;
        [SerializeField] private float timeToResetCombo;
        [SerializeField] private Transform startPos;
        [SerializeField] private RectTransform TextUI;
        [SerializeField] private TextMeshProUGUI text;
        private float currentTime = 0;

        
        private void OnEnable()
        {
            addHit.OnEnable();
            Fadeout();
        }
        
        private void OnDisable()
        {
            addHit.OnDisable();
        }
        
        private void Update()
        {
            if (_comboCounter > 0)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= timeToResetCombo)
                {
                    _comboCounter = 0;
                    Fadeout();
                    currentTime = 0;
                }
            }
        }
        
        public void AddHit()
        {
            _comboCounter++;
            text.text = "Hit " + _comboCounter.ToString() +"!";
            Popup();
            currentTime = 0;
        }
        
        private void Popup()
        {
            TextUI.transform.position = startPos.position;
            TextUI.transform.localScale = Vector3.zero;
            TextUI.transform.DOScale(1f, 0.15f).SetEase(Ease.OutBounce);
            //TextUI.transform.DOShakePosition(this.transform.position, 0.2f, );
            //shake text with dotween
            TextUI.DOShakePosition(1.5f, new Vector3(0, 40, 30), 6, 90, false, true);
        } 
        
        private void Fadeout()
        {
            TextUI.transform.DOScale(0f, 0.2f).SetEase(Ease.OutBounce);
            TextUI.transform.localScale = Vector3.zero;
        }
    }
}