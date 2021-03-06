﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace WizardWars
{
    public class CollectionManager : MonoBehaviour
    {

        #region Public Variables

        /// <summary>
        /// array of buttons which open one of the player's spell bars
        /// </summary>
        /// 
        public GameObject[] spellBarButtons;
   
        /// <summary>
        /// panel that displays information about the highlighted spell
        /// </summary>
        public GameObject tooltip;

        /// <summary>
        /// grid layout that holds the runes
        /// </summary>
        public GameObject runeGrid;

        /// <summary>
        /// prefab used by rune grid
        /// </summary>
        public GameObject runePrefab;

        /// <summary>
        /// slider the controls the size of runes in rune grid
        /// </summary>
        public GameObject sizeSlider;

        #endregion

        #region Private Variables

        /// <summary>
        /// runes that make up the player's current spell bar
        /// </summary>
        SpellSlotUI[] spellSlots;

        /// <summary>
        /// list of runes in rune grid
        /// </summary>
        List<GameObject> runes;

        /// <summary>
        /// size of runes in rune grid
        /// </summary>
        private Vector2 runeSize;

        /// <summary>
        /// library of all spells
        /// </summary>
        SpellStats[] library;

        #endregion

        // Use this for initialization
        void Start()
        {
            library = GameObject.FindGameObjectWithTag("Library").GetComponents<SpellStats>();
            runeSize = runePrefab.GetComponent<RectTransform>().rect.size;
            spellSlots = GetComponentsInChildren<SpellSlotUI>();

            InstantiateRunes();
            UpdateButtonLabels();
            SetSpellIcons(1, PlayerPrefsX.GetIntArray("Spells1"));
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Main Menu");
            }
        }

        #region Public Methods
        
        public void BackClick()
        {
            SceneManager.LoadScene("Main Menu");
        }

        public void PresetClick()
        {
            int index = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<PresetSelectButton>().index;
            int[] spellIds = new int[GlobalVariable.DECKSIZE];

            switch (index)
            {
                case 1:
                    spellIds = PlayerPrefsX.GetIntArray("Spells1");
                    break;
                case 2:
                    spellIds = PlayerPrefsX.GetIntArray("Spells2");
                    break;
                case 3:
                    spellIds = PlayerPrefsX.GetIntArray("Spells3");
                    break;
                case 4:
                    spellIds = PlayerPrefsX.GetIntArray("Spells4");
                    break;
                default:
                    break;
            }

            SetSpellIcons(index, spellIds);
        }

        /// <summary>
        /// Set size of runes with the slider
        /// </summary>
        public void SetSize()
        {
            switch ((int)sizeSlider.GetComponent<Slider>().value)
            {
                case 1:
                    ResizeRunes(new Vector3(0.5f, 0.5f, 1));
                    runeGrid.GetComponent<GridLayoutGroup>().cellSize = runeSize;
                    runeGrid.GetComponent<GridLayoutGroup>().spacing = new Vector2(20, 20);
                    break;
                case 2:
                    runeGrid.GetComponent<GridLayoutGroup>().cellSize = runeSize * 0.8f;
                    runeGrid.GetComponent<GridLayoutGroup>().spacing = new Vector2(20, 20) * 0.8f;
                    ResizeRunes(new Vector3(0.4f, 0.4f, 1));
                    break;
                case 3:
                    runeGrid.GetComponent<GridLayoutGroup>().cellSize = runeSize * 0.65f;
                    runeGrid.GetComponent<GridLayoutGroup>().spacing = new Vector2(20, 20) * 0.65f;
                    ResizeRunes(new Vector3(0.325f, 0.325f, 1));
                    break;
                case 4:
                    runeGrid.GetComponent<GridLayoutGroup>().cellSize = runeSize * 0.5f;
                    runeGrid.GetComponent<GridLayoutGroup>().spacing = new Vector2(20, 20) * 0.5f;
                    ResizeRunes(new Vector3(0.25f, 0.25f, 1));
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Iterate through runes and activate the ones that are not 
        /// in spell bar and deactivate the ones that are in current spell bar.
        /// This is to prevent duplicate runes in the same spell bar
        /// </summary>
        public void UpdateRunesIsDraggable()
        {
            int[] currentSpells = spellSlots.Select(x => x.GetComponent<SpellSlotUI>().GetSpellId()).ToArray();

            foreach (GameObject rune in runes)
            {
                int spellId = rune.GetComponent<RuneUI>().GetSpell().id;
                if (currentSpells.Contains(spellId))
                {
                    rune.GetComponent<RuneUI>().SetIsDraggable(false);
                }
                else
                {
                    rune.GetComponent<RuneUI>().SetIsDraggable(true);
                }
            }
        }

        #endregion

        #region Private Methods


        /// <summary>
        /// Set the spell icons to match the selected preset
        /// </summary>
        /// <param name="spellIds"></param>
        private void SetSpellIcons(int index, int[] spellIds)
        {
            for (int i = 0; i < spellIds.Length; i++)
            {
                spellSlots[i].SetIndex(index, i);
                spellSlots[i].SetSpellId(spellIds[i]);
                spellSlots[i].spellIcon.GetComponent<Image>().sprite = library[spellIds[i]].iconSprite;
            }
            UpdateRunesIsDraggable();
        }

        /// <summary>
        /// set text of spell bar buttons to their serialized name
        /// </summary>
        private void UpdateButtonLabels()
        {
            for (int i = 0; i < GlobalVariable.DECKCOUNT; i++)
            {
                spellBarButtons[i].GetComponentInChildren<Text>().text = PlayerPrefsX.GetStringArray("SpellBarNames")[i];
            }
        }

        /// <summary>
        /// scale runes based on the slider value
        /// </summary>
        /// <param name="scale"></param>
        private void ResizeRunes(Vector3 scale)
        {
            foreach (GameObject rune in runes)
            {
                rune.GetComponent<RectTransform>().localScale = scale;
            }
        }

        /// <summary>
        ///  instantiate runes for every spell in library
        /// </summary>
        private void InstantiateRunes()
        {
            runes = new List<GameObject>();
            int i = 0;

            foreach (SpellStats spell in library)
            {
                GameObject newRune = Instantiate(runePrefab, runeGrid.transform.position, runeGrid.transform.rotation, runeGrid.transform);
                newRune.GetComponent<RuneUI>().SetSpell(spell);
                newRune.GetComponent<RuneUI>().runeImage.GetComponent<Image>().sprite = spell.runeSprite;
                runes.Add(newRune);
                i++;
            }
        }

        #endregion
    }

}
