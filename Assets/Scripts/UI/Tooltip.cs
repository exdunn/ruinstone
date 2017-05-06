using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WizardWars
{
    public class Tooltip : MonoBehaviour
    {
        #region Public Variables

        public GameObject title;

        public GameObject body;

        #endregion

        #region Public Methods

        /// <summary>
        /// Set title text to input
        /// </summary>
        /// <param name="input"></param>
        public void SetTitle(string input)
        {
            title.GetComponent<Text>().text = input;
        }

        /// <summary>
        /// Set description text to input
        /// </summary>
        /// <param name="input"></param>
        public void SetBody(string input)
        {
            body.GetComponent<Text>().text = input;
        }

        /// <summary>
        /// Parses SpellStats spell and displays its information
        /// </summary>
        /// <param name="spell"></param>
        public void ParseSpellStats(SpellStats spell)
        {
            title.GetComponent<Text>().text = spell.GetName();
            
            string description = spell.GetDescription() + "\n";
            description += spell.GetDamage() > 0 ? "\nDamage: " + spell.GetDamage() : "";
            description += spell.GetCooldown() > 0 ? "\nCooldown: " + spell.GetCooldown() : "";
            //description += spell.GetRadius() > 0 ? "\nRadius: " + spell.GetRadius() : "";
            //description += spell.GetRange() > 0 ? "\nRange: " + spell.GetRange() : "";
            //description += spell.GetSpeed() > 0 ? "\nSpeed: " + spell.GetSpeed() : "";
            description += spell.GetDuration() > 0 ? "\nDuration: " + spell.GetDuration() + "s" : "";
            //description += spell.GetDelay() > 0 ? "\nDelay: " + spell.GetDelay() : "";
            body.GetComponent<Text>().text = description;
        }

        #endregion

    }

}
