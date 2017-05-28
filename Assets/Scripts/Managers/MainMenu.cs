using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WizardWars
{
    public class MainMenu : Photon.MonoBehaviour
    {

        #region Variables

        bool cinematic = false;

        #endregion

        #region monobehaviour 

        // Use this for initialization
        void Start()
        {
            if (PlayerPrefs.GetInt("Init") == 0) {

                Initialize();
            }

            AudioListener.volume = PlayerPrefs.GetFloat("Vol");

            PhotonNetwork.playerName = PlayerPrefs.GetString("PlayerName");

            // if recording mode then go to cinematic scene
            if (cinematic) {

                SceneManager.LoadScene("Cinematic Scene");
            }     
        }

        #endregion

        #region public methods

        /// <summary>
        /// Go to the create lobby menu
        /// </summary>
        public void CreateClick()
        {
            SceneManager.LoadScene("Create Menu");
        }

        /// <summary>
        /// Go to the join lobby menu
        /// </summary>
        public void JoinClick()
        {
            SceneManager.LoadScene("Join Menu");
        }

        /// <summary>
        /// Go to the controls menu
        /// </summary>
        public void ControlsClick()
        {
            SceneManager.LoadScene("Controls Menu");
        }

        /// <summary>
        /// Exit the game
        /// </summary>
        public void ExitClick()
        {
            Application.Quit();
        }

        /// <summary>
        /// Go to Collection scene
        /// </summary>
        public void CollectionClick()
        {
            SceneManager.LoadScene("Spell Collection");
        }

        #endregion

        #region Private Methods

        // This function is used to initialize player preferences if this is their first time opening the game
        private void Initialize()
        {
            // initialize spell presets
            PlayerPrefsX.SetStringArray("SpellBarNames", new string[] { "Spells 1", "Spells 2", "Spells 3", "Spells 4" });
            PlayerPrefsX.SetIntArray("Spells1", new int[] { 0, 1, 2, 8 });
            PlayerPrefsX.SetIntArray("Spells2", new int[] { 1, 2, 3, 8 });
            PlayerPrefsX.SetIntArray("Spells3", new int[] { 1, 6, 2, 8 });
            PlayerPrefsX.SetIntArray("Spells4", new int[] { 0, 1, 6, 8 });

            // initialize volume setting
            PlayerPrefs.SetFloat("Vol", 0.5f);

            // initialize music setting
            PlayerPrefs.SetInt("Theme", 1);

            // Set init to 1 so this function isn't called again
            PlayerPrefs.SetInt("Init", 1);
        }

        #endregion
    }
}