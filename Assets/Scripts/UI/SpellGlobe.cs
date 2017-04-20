using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace WizardWars
{
    public class SpellGlobe : MonoBehaviour
        , IPointerEnterHandler
        , IPointerExitHandler
    {


        #region Public Variables

        #endregion

        #region Private Variables

        /// <summary>
        /// Spell icon image
        /// </summary>
        private GameObject icon;

        /// <summary>
        /// Radial fill image that is set to 1 when player casts the spell then
        /// contracts to 0 as as cooldownTimer increases
        /// </summary>
        private GameObject fill;

        /// <summary>
        /// A tooltip info panel that pops up when the player hovers over the spell globe
        /// </summary>
        private GameObject tooltip;

        /// <summary>
        /// Timer that starts when a player uses the spell and resets when cooldownTimer equals the spell's cooldown
        /// </summary>
        private float cooldownTimer;

        #endregion

        #region MonoBehaviour CallBacks

        // Use this for initialization
        void Start () {

            icon = transform.GetChild(0).gameObject;
            fill = transform.GetChild(1).gameObject;
            tooltip = transform.GetChild(2).gameObject;

	    }
	
	    // Update is called once per frame
	    void Update () {

            // set fill to display remaining cooldown
            fill.GetComponent<Image>().fillAmount = cooldownTimer;


	    }

        #endregion


        #region Public Methods

        /// <summary>
        /// Set icon's image to the designated sprite
        /// </summary>
        /// <param name="sprite"></param>
        public void SetIcon(Sprite sprite)
        {
            icon.GetComponent<Image>().sprite = sprite;
        }

        /// <summary>
        /// Set the name text in tooltip
        /// </summary>
        /// <param name="name"></param>
        public void SetTooltipName(string name)
        {
            if (!name.Equals("") && !name.Equals(null))
            { 
                tooltip.transform.GetChild(0).gameObject.GetComponent<Text>().text = name;
            }
        }

        /// <summary>
        /// Set the desciption text in tooltip
        /// </summary>
        /// <param description="description"></param>
        public void SetTooltipDescription(string description)
        {
            if (!description.Equals("") && !description.Equals(null))
            {
                tooltip.transform.GetChild(0).gameObject.GetComponent<Text>().text = description;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            tooltip.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tooltip.SetActive(false);
        }

        #endregion

    }

}
