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

        /// <summary>
        /// Create presets in player prefs
        /// </summary>
        private void InitializePresets()
        {
            PlayerPrefsX.SetStringArray("SpellBarNames", new string[] { "Spells 1", "Spells 2", "Spells 3", "Spells 4" });

            PlayerPrefsX.SetIntArray("Spells1", new int[] { 0, 1, 2, 8 });
            PlayerPrefsX.SetIntArray("Spells2", new int[] { 1, 2, 3, 8 });
            PlayerPrefsX.SetIntArray("Spells3", new int[] { 1, 6, 2, 8 });
            PlayerPrefsX.SetIntArray("Spells4", new int[] { 0, 1, 6, 8 });
        }

        #endregion



        // Use this for initialization
        void Start()
        {
            // set volume to 50%
            AudioListener.volume = 0.5f;

            //InitializePresets();

            PhotonNetwork.playerName = PlayerPrefs.GetString("PlayerName");

            // if recording mode then go to cinematic scene
            if (cinematic)
                SceneManager.LoadScene("Cinematic Scene");
        }
    }
}