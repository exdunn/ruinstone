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
            runeGrid.GetComponent<GridLayoutGroup>().padding = new RectOffset(20, 20, 20, 20);
            library = GameObject.FindGameObjectWithTag("Library");

            InstantiateRunes();

	    }
	

        #endregion

        #region Public Methods

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

            //ResizePanel();
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
                newRune.GetComponent<RuneUI>().spriteNormal = spell.GetRuneSprite();
                newRune.GetComponent<RuneUI>().spriteHighlighted = spell.GetHighlightedRuneSprite();
                newRune.GetComponent<RuneUI>().runeImage.GetComponent<Image>().sprite = spell.GetRuneSprite();
                runes.Add(newRune);
                i++;
            }
        }

        /// <summary>
        /// determine the x/y coordinates of a rune's instantiation
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        /*private Vector2 RunePosition(int i)
        {
            float xCoord = 0;
            float yCoord = 0;
            switch ((int)sizeSlider.GetComponent<Slider>().value)
            {
                case 1:
                    xCoord = i % 3 * 150 - 150;
                    yCoord = i / 3 * -170 - 100;
                    break;

                case 2:
                    xCoord = i % 4 * 140 - 220;
                    yCoord = i / 4 * -160 - 50;
                    break;

                case 3:
                    xCoord = i % 6 * 140 - 360;
                    yCoord = i / 6 * -170 + 50;
                    break;

                case 4:
                    xCoord = i % 9 * 150 - 600;
                    yCoord = i / 9 * -170 + 220;
                    break;

                default:
                    break;
            }

            return new Vector2(xCoord, yCoord);
        }*/

        /// <summary>
        /// resize the height of background panel based on the number and size of runes
        /// </summary>
        /*private void ResizePanel()
        {
            float scalar = GameObject.FindGameObjectWithTag("Rune").GetComponent<RectTransform>().rect.height;

            float xCoord = runePanelBackground.GetComponent<RectTransform>().rect.x;
            float yCoord = runePanelBackground.GetComponent<RectTransform>().rect.y;
            float width = runePanelBackground.GetComponent<RectTransform>().rect.width;
            float height = runePanelBackground.GetComponent<RectTransform>().rect.height;
            
            scalar = scalar * runes.Count / (3 + 3 * -(-(int)sizeSlider.GetComponent<Slider>().value + 1));
            float bottom = runePanelBackground.GetComponent<RectTransform>().rect.bottom - scalar;

            runePanelBackground.GetComponent<RectTransform>().offsetMin = new Vector2(GetComponent<RectTransform>().offsetMin.x, -100);
            //runePanelBackground.GetComponent<RectTransform>().rect.Set(xCoord, yCoord, width, height);

            //runePanelBackground.GetComponent<RectTransform>().rect.bottom -= scalar * runes.Count / (3 + 3 * ((int)sizeSlider.GetComponent<Slider>().value - 1));
        }*/

        #endregion
    }
}

