using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WizardWars
{
    public class ControlsManager : MonoBehaviour {

        #region Public Variables

        [Tooltip("Tab which displays user settings")]
        public GameObject userTab;

        [Tooltip("Tab which displays input hotkeys")]
        public GameObject controlsTab;

        [Tooltip("Tab which displays sound settings")]
        public GameObject soundTab;

        #endregion

        #region MonoBehaviour Callbacks

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Main Menu");
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Open the user tab
        /// </summary>
        public void UserClick()
        {
            userTab.SetActive(true);
            controlsTab.SetActive(false);
            soundTab.SetActive(false);
        }

        /// <summary>
        /// Open the controls tab
        /// </summary>
        public void ControlsClick()
        {
            userTab.SetActive(false);
            controlsTab.SetActive(true);
            soundTab.SetActive(false);
        }

        /// <summary>
        /// Open the sound tab
        /// </summary>
        public void SoundClick()
        {
            userTab.SetActive(false);
            controlsTab.SetActive(false);
            soundTab.SetActive(true);
        }

        #endregion

    }

}
