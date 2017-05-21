using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WizardWars
{
    public class ControlsManager : MonoBehaviour {

        #region Public Variables

        public GameObject userTab;
        public GameObject controlsTab;
        public GameObject soundTab;
        public GameObject colorButton;

        #endregion

        #region private variables 

        int robeColor;

        #endregion

        #region MonoBehaviour Callbacks

        void Start()
        {
            if (System.String.IsNullOrEmpty(PlayerPrefs.GetInt("Color").ToString())) 
            {
                robeColor = 0;
            }
            else
            {
                robeColor = PlayerPrefs.GetInt("Color");
            }
            colorButton.GetComponent<Image>().color = GetColor(robeColor);
        }

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
            robeColor = robeColor >= 3 ? 0 : ++robeColor;

            Debug.Log("color: " + robeColor);

            PlayerPrefs.SetInt("Color", robeColor);
            colorButton.GetComponent<Image>().color = GetColor(robeColor);
        }

        private Color GetColor(int i)
        {
            switch (i)
            {
                case 0:
                    return Color.black;
                case 1:
                    return Color.red;
                case 2:
                    return new Color32(117, 6, 188, 255);
                case 3:
                    return Color.green;
                default:
                    return Color.black;
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
