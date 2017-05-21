using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WizardWars
{
    public class MainMenu : Photon.MonoBehaviour
    {

        #region Variables

        bool ready = false;

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
            if (ready)
                SceneManager.LoadScene("Spell Collection");
            else
            {
                // not ready
            }

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Create presets in player prefs
        /// </summary>
        private void InitializePresets()
        {
            PlayerPrefsX.SetStringArray("SpellBarNames", new string[] { "Spells 1", "Spells 2", "Spells 3", "Spells 4", "Spells 5" });

            PlayerPrefsX.SetIntArray("Spells1", new int[] { 0, 1, 2, 8 });
            PlayerPrefsX.SetIntArray("Spells2", new int[] { 6, 3, 8, 1 });
            PlayerPrefsX.SetIntArray("Spells3", new int[] { 1, 6, 2, 8 });
            PlayerPrefsX.SetIntArray("Spells4", new int[] { 5, 6, 7, 8 });
        }

        #endregion



        // Use this for initialization
        void Start()
        {
            // set volume to 50%
            AudioListener.volume = 0.5f;

            InitializePresets();

            PhotonNetwork.playerName = PlayerPrefs.GetString("PlayerName");
        }
    }
}