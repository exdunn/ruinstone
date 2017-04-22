using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WizardWars
{
    public class ButtonUI : MonoBehaviour
    {

        #region Private Variables

        /// <summary>
        /// text printed on the button
        /// </summary>
        Text buttonText;

        #endregion

        #region MonoBehaviour Callbacks

        // Use this for initialization
        void Start()
        {
            buttonText = GetComponentInChildren<Text>();
        }

        #endregion

        #region Public Methods

        public void SetText(string input)
        {
            buttonText.text = input;
        }

        #endregion
    }

}
