using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardWars
{
    public class PlayerAccount : MonoBehaviour
    {
        /// <summary>
        /// Serializable spell bar list class
        /// </summary>
        [System.Serializable]
        public class SpellBarList
        {
            public SpellBar[] spellBar;

            public SpellBarList ()
            {
                spellBar = new SpellBar[GlobalVariable.DECKCOUNT];
            }
        }

        [SerializeField]
        private SpellBarList spellBarList;

        SpellStats[] library;

        #region MonoBehaviour Callbacks

        // Use this for initialization
        void Awake()
        {
  
            library = GameObject.FindGameObjectWithTag("Library").GetComponents<SpellStats>();

            PlayerPrefsX.SetStringArray("SpellBarNames", new string[] { "Spell Bar 1", "Spell Bar Two", "Spell Bar Three", "Spell Bar Four", "Spell Bar Five" });

            PlayerPrefsX.SetIntArray("SpellListOne", new int[]{ 7,2,3,4});
            PlayerPrefsX.SetIntArray("SpellListTwo", new int[] { 2, 2, 3, 4 });
            PlayerPrefsX.SetIntArray("SpellListThree", new int[] { 3, 2, 3, 4 });
            PlayerPrefsX.SetIntArray("SpellListFour", new int[] { 4, 2, 3, 4 });
            PlayerPrefsX.SetIntArray("SpellListFive", new int[] { 5, 6, 7, 7 });
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion
    }
}

