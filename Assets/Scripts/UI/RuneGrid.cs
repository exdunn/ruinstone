using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WizardWars
{

    public class RuneGrid : MonoBehaviour {

        #region Public Variables

        public GameObject runePanel;
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

        #endregion

        #region MonoBehaviour Callbacks

        // Use this for initialization
        void Start () {

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
                    runePanel.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    break;
                case 2:
                    runePanel.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.8f, 1);
                    break;
                case 3:
                    runePanel.GetComponent<RectTransform>().localScale = new Vector3(0.55f, 0.55f, 1);
                    break;
                case 4:
                    runePanel.GetComponent<RectTransform>().localScale = new Vector3(0.36f, 0.36f, 1);
                    break;

                default:
                    break;
            }

            for (int i = 0; i < runes.Count; i++)
            {
                runes[i].GetComponent<RectTransform>().anchoredPosition = RunePosition(i);
            }
        }

        #endregion

        #region Private Methods 

        private void InstantiateRunes()
        {
            runes = new List<GameObject>();
            int i = 0;

            foreach (SpellStats spell in library.GetComponents<SpellStats>())
            {
                GameObject newRune = Instantiate(runePrefab, runePanel.transform.position, runePanel.transform.rotation, runePanel.transform);
                newRune.GetComponent<RectTransform>().anchoredPosition = RunePosition(i);
                newRune.GetComponent<RuneUI>().spriteNormal = spell.GetRuneSprite();
                newRune.GetComponent<RuneUI>().spriteHighlighted = spell.GetHighlightedRuneSprite();
                newRune.GetComponent<RuneUI>().runeImage.GetComponent<Image>().sprite = spell.GetRuneSprite();
                runes.Add(newRune);
                i++;
            }
        }

        private Vector2 RunePosition(int i)
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
        }

        #endregion
    }
}

