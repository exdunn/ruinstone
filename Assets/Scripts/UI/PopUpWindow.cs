using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WizardWars
{
    public class PopUpWindow : MonoBehaviour
    {

        #region Public Variables

        [Tooltip("The background content that is active when pop up window is inactive and inactive when pop up window is active")]
        public GameObject[] backgroundContent;

        #endregion

        #region Public Methods

        /// <summary>
        /// Deactivate the pop up window and activate the background content
        /// </summary>
        public void CloseClick()
        {
            foreach (GameObject item in backgroundContent)
            {
                item.SetActive(true);
            }
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Open the pop up window and display 'message'
        /// </summary>
        public void Show(string message)
        {
            gameObject.GetComponentInChildren<Text>().text = message;
            gameObject.SetActive(true);
        }

        #endregion

        #region MonoBehavious Callbacks

        #endregion
    }
}