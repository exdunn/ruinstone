using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WizardWars
{
    public class PlayerLabel : MonoBehaviour
    {
        #region Public Variables

        /// <summary>
        /// Text element that displays the game name 
        /// </summary>
        public Text nameLabel;

        /// <summary>
        /// Text element that displays the number of players out of the maximum number of players 
        /// </summary>
        public Text classLabel;

        /// <summary>
        /// Text element that displays the game mode
        /// </summary>
        public Text teamLabel;

        #endregion

        #region Private Variables

        #endregion
        
        #region Public Methods

        /// <summary>
        /// Set nameLabel to given string
        /// </summary>
        public void SetNameLabel(string value)
        {
            if (!(value.Equals("") || value.Equals(null)))
            {
                nameLabel.text = value;
            }
            else
            {
                nameLabel.text = "Player";
            }
            
        }

        #endregion
        
        #region Private Methods

        #endregion

    }
}
