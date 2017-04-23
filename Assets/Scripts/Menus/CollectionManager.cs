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

            
        }

        // Update is called once per frame
        void Update()
        {
            UpdateButtonLabels();
        }

        #region Public Methods

        public void SpellBar1Click()
        {
            int[] spellIds = PlayerPrefsX.GetIntArray("SpellListOne");
          
            for (int i = 0; i < spellIds.Length; i++)
            {
                runeSlots[i].GetComponent<RuneSlotUI>().spellIcon.GetComponent<Image>().sprite = library[spellIds[i]].GetIconSprite();
            }
        }

        public void SpellBar2Click()
        {
            int[] spellIds = PlayerPrefsX.GetIntArray("SpellListTwo");

            for (int i = 0; i < spellIds.Length; i++)
            {
                runeSlots[i].GetComponent<RuneSlotUI>().spellIcon.GetComponent<Image>().sprite = library[spellIds[i]].GetIconSprite();
            }
        }

        public void SpellBar3Click()
        {
            int[] spellIds = PlayerPrefsX.GetIntArray("SpellListThree");

            for (int i = 0; i < spellIds.Length; i++)
            {
                runeSlots[i].GetComponent<RuneSlotUI>().spellIcon.GetComponent<Image>().sprite = library[spellIds[i]].GetIconSprite();
            }
        }

        public void SpellBar4Click()
        {
            int[] spellIds = PlayerPrefsX.GetIntArray("SpellListFour");

            for (int i = 0; i < spellIds.Length; i++)
            {
                runeSlots[i].GetComponent<RuneSlotUI>().spellIcon.GetComponent<Image>().sprite = library[spellIds[i]].GetIconSprite();
            }
        }

        public void SpellBar5Click()
        {
            int[] spellIds = PlayerPrefsX.GetIntArray("SpellListFive");

            for (int i = 0; i < spellIds.Length; i++)
            {
                runeSlots[i].GetComponent<RuneSlotUI>().spellIcon.GetComponent<Image>().sprite = library[spellIds[i]].GetIconSprite();
            }
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
