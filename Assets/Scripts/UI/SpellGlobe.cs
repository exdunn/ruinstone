using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WizardWars
{
    public class SpellGlobe : MonoBehaviour {


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
        /// Timer that starts when a player uses the spell and resets when cooldownTimer equals the spell's cooldown
        /// </summary>
        private float cooldownTimer;

        #endregion

        #region MonoBehaviour CallBacks

        // Use this for initialization
        void Start () {

            icon = transform.GetChild(0).gameObject;
            fill = transform.GetChild(1).gameObject;

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

        #endregion

    }

}
