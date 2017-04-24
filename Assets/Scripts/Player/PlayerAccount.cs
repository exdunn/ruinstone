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

            if (PlayerPrefsX.GetStringArray("SpellBarNames").Length == 0)
            {
                PlayerPrefsX.SetStringArray("SpellBarNames", new string[] { "Preset 1", "Preset 2", "Preset 3", "Preset 4", "Preset 5" });

                PlayerPrefsX.SetIntArray("SpellListOne", new int[]{ 1,2,3,4});
                PlayerPrefsX.SetIntArray("SpellListTwo", new int[] { 2, 3, 4, 5 });
                PlayerPrefsX.SetIntArray("SpellListThree", new int[] { 3, 4, 5, 6 });
                PlayerPrefsX.SetIntArray("SpellListFour", new int[] { 5, 6, 7, 8 });
                PlayerPrefsX.SetIntArray("SpellListFive", new int[] { 1, 3, 5, 7 });
            }  
        }

        #endregion
    }
}