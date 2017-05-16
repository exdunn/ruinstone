using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WizardWars
{
    public class MainMenu : Photon.MonoBehaviour
    {

        #region Public Variables

        #endregion

        #region Private Variables

        #endregion

        #region Public Methods

        /// <summary>
        /// Go to the create lobby menu
        /// </summary>
        public void CreateClick ()
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
            SceneManager.LoadScene("Athenaeum");
        }

        #endregion

        #region Private Methods
        
        /// <summary>
        /// Create presets in player prefs
        /// </summary>
        private void InitializePresets()
        {
            PlayerPrefsX.SetStringArray("SpellBarNames", new string[] { "Preset 1", "Preset 2", "Preset 3", "Preset 4", "Preset 5" });

            PlayerPrefsX.SetIntArray("Preset1", new int[] { 1, 2, 3, 4 });
            PlayerPrefsX.SetIntArray("Preset2", new int[] { 2, 3, 4, 5 });
            PlayerPrefsX.SetIntArray("Preset3", new int[] { 3, 4, 5, 6 });
            PlayerPrefsX.SetIntArray("Preset4", new int[] { 5, 6, 7, 8 });
        }

        #endregion



        // Use this for initialization
        void Start ()
        {
            AudioListener.volume = 0.5f;
            InitializePresets();
            PhotonNetwork.playerName = PlayerPrefs.GetString("PlayerName");
	    }
    }

}
