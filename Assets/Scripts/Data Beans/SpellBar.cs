using System.Collections;
using System.Collections.Generic;

namespace WizardWars
{
    [System.Serializable]
    public class SpellBar
    {

        #region Private Variables

        public int[] spells;
        public string barName;

        #endregion

        #region Constructors

        public SpellBar()
        {
            spells = new int[GlobalVariable.DECKSIZE];
            for (int i = 0; i < spells.Length; i++)
            {
                spells[i] = i;
            }

            barName = "Gandalf's Bag of Tricks";
        }

        public SpellBar(int[] _spells, string name)
        {
            spells = _spells;
            barName = name;
        }

        #endregion


        #region public methods 


        #endregion
    }
}

