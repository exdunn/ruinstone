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
            DontDestroyOnLoad(transform);
  
            library = GameObject.FindGameObjectWithTag("Library").GetComponents<SpellStats>();

            spellBarList = new SpellBarList();              
        }

        #endregion

        #region Public Methods

        public void InsertSpellBar(int index, SpellBar bar)
        {
            spellBarList.spellBar[index] = bar;
        }

        public SpellBarList GetSpellBarList()
        {
            return spellBarList;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}

