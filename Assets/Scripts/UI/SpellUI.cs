using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace WizardWars
{
    public class SpellUI : MonoBehaviour
        , IPointerEnterHandler
        , IPointerExitHandler
    {


        #region Public Variables

        public GameObject tooltip;
        public GameObject icon;
        public GameObject fill;
        private float cooldownTimer;

        public SpellStats spell
        {
            get; set;
        }

        #endregion

        #region MonoBehaviour CallBacks

        // Use this for initialization
        void Start () {

            
	    }

        private void FixedUpdate()
        {
            // set fill to display remaining cooldown
            if (cooldownTimer <= spell.cooldown)
            {
                cooldownTimer += Time.deltaTime;
                fill.GetComponent<Image>().fillAmount = (spell.cooldown - cooldownTimer) / spell.cooldown;
            }
        }

        #endregion

        #region Public Methods

        public void ResetCooldownTimer()
        {
            cooldownTimer = 0;
        }

        public void SetIcon(Sprite sprite)
        {
            icon.GetComponent<Image>().sprite = sprite;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            tooltip.SetActive(true);
            tooltip.GetComponent<Tooltip>().ParseSpellStats(spell);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tooltip.SetActive(false);
            tooltip.GetComponent<Tooltip>().ParseSpellStats(spell);
        }

        #endregion

        #region private methods

        // returns the fraction of the spell's remaining cooldown left
        private float GetCooldownRemaining()
        {
            return 1 - (spell.cooldown - cooldownTimer) / spell.cooldown;
        }

        IEnumerator OnCooldown()
        {


            yield return null;
        }

        #endregion

    }

}
