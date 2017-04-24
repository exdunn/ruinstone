using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WizardWars
{
    public class CollectionManager : MonoBehaviour
    {

        #region Public Variables

        public GameObject[] spellBarButtons;
        public GameObject[] runeSlots;
        public GameObject tooltip;

        #endregion

        #region Private Variables

        PlayerAccount player;
        SpellStats[] library;

        #endregion

        // Use this for initialization
        void Start()
        {
            library = GameObject.FindGameObjectWithTag("Library").GetComponents<SpellStats>();
            player = GameObject.FindGameObjectWithTag("Account").GetComponent<PlayerAccount>();
            Debug.Log("player: " + player);

            SpellBar1Click();
            UpdateButtonLabels();
        }

        #region Public Methods

        public void SpellBar1Click()
        {
            int[] spellIds = PlayerPrefsX.GetIntArray("SpellListOne");
            

          
            for (int i = 0; i < spellIds.Length; i++)
            {
                runeSlots[i].GetComponent<RuneSlotUI>().SetSpellId(spellIds[i]);
                runeSlots[i].GetComponent<RuneSlotUI>().SetIndex(0, i);
                runeSlots[i].GetComponent<RuneSlotUI>().spellIcon.GetComponent<Image>().sprite = library[spellIds[i]].GetIconSprite();
            }
            UpdateButtonLabels();
        }

        public void SpellBar2Click()
        {
            int[] spellIds = PlayerPrefsX.GetIntArray("SpellListTwo");

            for (int i = 0; i < spellIds.Length; i++)
            {
                runeSlots[i].GetComponent<RuneSlotUI>().SetSpellId(spellIds[i]);
                runeSlots[i].GetComponent<RuneSlotUI>().SetIndex(1, i);
                runeSlots[i].GetComponent<RuneSlotUI>().spellIcon.GetComponent<Image>().sprite = library[spellIds[i]].GetIconSprite();
            }
            UpdateButtonLabels();
        }

        public void SpellBar3Click()
        {
            int[] spellIds = PlayerPrefsX.GetIntArray("SpellListThree");

            for (int i = 0; i < spellIds.Length; i++)
            {
                runeSlots[i].GetComponent<RuneSlotUI>().SetSpellId(spellIds[i]);
                runeSlots[i].GetComponent<RuneSlotUI>().SetIndex(2, i);
                runeSlots[i].GetComponent<RuneSlotUI>().spellIcon.GetComponent<Image>().sprite = library[spellIds[i]].GetIconSprite();
            }
            UpdateButtonLabels();
        }

        public void SpellBar4Click()
        {
            int[] spellIds = PlayerPrefsX.GetIntArray("SpellListFour");

            for (int i = 0; i < spellIds.Length; i++)
            {
                runeSlots[i].GetComponent<RuneSlotUI>().SetSpellId(spellIds[i]);
                runeSlots[i].GetComponent<RuneSlotUI>().SetIndex(3, i);
                runeSlots[i].GetComponent<RuneSlotUI>().spellIcon.GetComponent<Image>().sprite = library[spellIds[i]].GetIconSprite();
            }
            UpdateButtonLabels();
        }

        public void SpellBar5Click()
        {
            int[] spellIds = PlayerPrefsX.GetIntArray("SpellListFive");

            for (int i = 0; i < spellIds.Length; i++)
            {
                runeSlots[i].GetComponent<RuneSlotUI>().SetSpellId(spellIds[i]);
                runeSlots[i].GetComponent<RuneSlotUI>().SetIndex(4, i);
                runeSlots[i].GetComponent<RuneSlotUI>().spellIcon.GetComponent<Image>().sprite = library[spellIds[i]].GetIconSprite();
            }
            UpdateButtonLabels();
        }


        public void UpdateTooltip(SpellStats spell)
        {
            tooltip.GetComponent<Tooltip>().SetTitle(spell.GetName());
            string body = spell.GetDescription() + "\n";
            body += spell.GetDamage() > 0 ? "\nDamage: " + spell.GetDamage() : "";
            body += spell.GetCooldown() > 0 ? "\nCooldown: " + spell.GetCooldown() : "";
            //body += spell.GetRadius() > 0 ? "\nRadius: " + spell.GetRadius() : "";
            //body += spell.GetRange() > 0 ? "\nRange: " + spell.GetRange() : "";
            //body += spell.GetSpeed() > 0 ? "\nSpeed: " + spell.GetSpeed() : "";
            body += spell.GetDuration() > 0 ? "\nDuration: " + spell.GetDuration() + "s" : "";
            //body += spell.GetDelay() > 0 ? "\nDelay: " + spell.GetDelay() : "";
            tooltip.GetComponent<Tooltip>().SetBody(body);
        }

        #endregion

        #region Private Methods

        private void UpdateButtonLabels()
        {
            for (int i = 0; i < GlobalVariable.DECKCOUNT; i++)
            {
                spellBarButtons[i].GetComponent<ButtonUI>().SetText(PlayerPrefsX.GetStringArray("SpellBarNames")[i]);
            }
        }

        #endregion
    }

}
