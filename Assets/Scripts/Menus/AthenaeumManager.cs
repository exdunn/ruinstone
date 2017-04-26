using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace WizardWars
{
    public class AthenaeumManager : MonoBehaviour
    {

        #region Public Variables

        /// <summary>
        /// array of buttons which open one of the player's spell bars
        /// </summary>
        /// 
        public GameObject[] spellBarButtons;

        /// <summary>
        /// runes that make up the player's current spell bar
        /// </summary>
        public GameObject[] runeSlots;

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

            InstantiateRunes();
            SpellBar1Click();
            UpdateButtonLabels();
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
            UpdateRunesIsDraggable();
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
            UpdateRunesIsDraggable();
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
            UpdateRunesIsDraggable();
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
            UpdateRunesIsDraggable();
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
            UpdateRunesIsDraggable();
        }

        /// <summary>
        /// set text in tooltip
        /// </summary>
        /// <param name="spell"></param>
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
            int[] currentSpells = runeSlots.Select(x => x.GetComponent<RuneSlotUI>().GetSpellId()).ToArray();

            foreach (GameObject rune in runes)
            {
                int spellId = rune.GetComponent<RuneUI>().GetSpell().GetId();
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
        /// set text of spell bar buttons to their serialized name
        /// </summary>
        private void UpdateButtonLabels()
        {
            for (int i = 0; i < GlobalVariable.DECKCOUNT; i++)
            {
                spellBarButtons[i].GetComponent<ButtonUI>().SetText(PlayerPrefsX.GetStringArray("SpellBarNames")[i]);
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
                newRune.GetComponent<RuneUI>().runeImage.GetComponent<Image>().sprite = spell.GetRuneSprite();
                runes.Add(newRune);
                i++;
            }
        }

        #endregion
    }

}
