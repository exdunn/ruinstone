using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WizardWars
{
    public class ControlsManager : MonoBehaviour {

        enum RobeColor {
            black, red, green, blue
        };

        #region Public Variables

        public GameObject userTab;
        public GameObject controlsTab;
        public GameObject soundTab;
        public GameObject colorButton;

        #endregion

        #region private variables 

        RobeColor rc = RobeColor.black;

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

        public void ColorClick()
        {
            switch (rc)
            {
                case RobeColor.black:
                    colorButton.GetComponent<Image>().color = Color.red;
                    PlayerPrefs.SetString("robe", "red");
                    rc++;
                    break;
                case RobeColor.red:
                    colorButton.GetComponent<Image>().color = Color.green;
                    PlayerPrefs.SetString("robe", "green");
                    rc++;
                    break;
                case RobeColor.green:
                    colorButton.GetComponent<Image>().color = Color.blue;
                    PlayerPrefs.SetString("robe", "blue");
                    rc++;
                    break;
                case RobeColor.blue:
                    colorButton.GetComponent<Image>().color = Color.black;
                    PlayerPrefs.SetString("robe", "black");
                    rc = 0;
                    break;
                default:
                    break;
            }
        }

        public void SetVolume(float value)
        {
            AudioListener.volume = value;
        }

        public void ToggleMusic(bool state)
        {
            AudioSource theme = GameObject.Find("RuinStone Theme").GetComponent<AudioSource>();

            if (theme == null)
            {
                Debug.LogError("Cannot find audio source");
                return;
            }

            theme.enabled = state;
        }

        #endregion

    }

}
