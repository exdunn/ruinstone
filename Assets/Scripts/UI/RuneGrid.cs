using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WizardWars
{

    public class RuneGrid : MonoBehaviour {

        #region Public Variables

        public GameObject runeGrid;
        public GameObject runePrefab;
        public GameObject sizeSlider;
        public GameObject tooltip;

        #endregion

        #region Private Variables

        /// <summary>
        /// How large the runes in rune panel are.  Player controls size with the size scroll bar.
        /// </summary>
        private int size;

        private GameObject library;

        private List<GameObject> runes;

        private Vector2 runeSize;

        #endregion

        #region MonoBehaviour Callbacks

        // Use this for initialization
        void Start () {

            runeSize = runePrefab.GetComponent<RectTransform>().rect.size;
            runeGrid.GetComponent<GridLayoutGroup>().padding = new RectOffset(10, 10, 0, 10);
            library = GameObject.FindGameObjectWithTag("Library");

            InstantiateRunes();
	    }
	

        #endregion

        #region Public Methods

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

            foreach (SpellStats spell in library.GetComponents<SpellStats>())
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

