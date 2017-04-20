using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WizardWars
{
    public class Tooltip : MonoBehaviour
    {
        #region Public Variables

        public GameObject title;

        public GameObject description;

        #endregion

        #region Public Methods

        /// <summary>
        /// Set title text to input
        /// </summary>
        /// <param name="input"></param>
        public void SetTitle(string input)
        {
            title.GetComponent<Text>().text = input;
        }

        /// <summary>
        /// Set description text to input
        /// </summary>
        /// <param name="input"></param>
        public void SetDescription(string input)
        {
            description.GetComponent<Text>().text = input;
        }

        #endregion

    }

}
