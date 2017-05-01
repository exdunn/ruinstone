using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardWars
{
    public class PlayerAccount : MonoBehaviour
    {
        SpellStats[] library;

        #region MonoBehaviour Callbacks

        // Use this for initialization
        void Awake()
        {
  
            library = GameObject.FindGameObjectWithTag("Library").GetComponents<SpellStats>();

            InitializePresets();

            if (PlayerPrefsX.GetStringArray("SpellBarNames").Length == 0)
            {
                InitializePresets();
            }  
        }

        #endregion

        private void InitializePresets()
        {
            PlayerPrefsX.SetStringArray("SpellBarNames", new string[] { "Preset 1", "Preset 2", "Preset 3", "Preset 4", "Preset 5" });

            PlayerPrefsX.SetIntArray("Preset1", new int[] { 1, 2, 3, 4 });
            PlayerPrefsX.SetIntArray("Preset2", new int[] { 2, 3, 4, 5 });
            PlayerPrefsX.SetIntArray("Preset3", new int[] { 3, 4, 5, 6 });
            PlayerPrefsX.SetIntArray("Preset4", new int[] { 5, 6, 7, 8 });
        }
    }
}