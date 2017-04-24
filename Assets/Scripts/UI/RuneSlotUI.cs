using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace WizardWars
{
    public class RuneSlotUI : MonoBehaviour
        , IPointerEnterHandler
        , IPointerExitHandler
        , IDropHandler
    {

        #region Public Variables

        public Sprite spriteNormal;
        public Sprite spriteHighlighted;
        public GameObject spellIcon;
        public GameObject background;

        #endregion

        #region Private Variables

        int spellId;

        /// <summary>
        /// The index of the rune slot in player's spell presets
        /// First number is the spell bar
        /// Second number is the position in the spell bar
        /// </summary>
        int[] index = new int[2];

        #endregion

        #region MonoBehaviour CallBacks

        public void OnPointerExit(PointerEventData eventData)
        {
            background.GetComponent<Image>().sprite = spriteNormal;
            background.GetComponent<Image>().SetNativeSize();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            background.GetComponent<Image>().sprite = spriteHighlighted;
            background.GetComponent<Image>().SetNativeSize();

            Debug.Log("id: " + spellId);
            // Search spell library for spell with spellId and use it to set the tooltip
            GetComponentInParent<CollectionManager>().UpdateTooltip(GameObject.FindGameObjectWithTag("Library").GetComponents<SpellStats>()[spellId]);
        }

        public void OnDrop(PointerEventData eventData)
        {
            spellIcon.GetComponent<Image>().sprite = RuneUI.itemBeingDragged.GetComponent<RuneUI>().GetSpell().GetIconSprite();


            string playerPrefIndex = "";

            switch (index[0])
            {
                case 0:
                    playerPrefIndex = "SpellListOne";
                    break;

                case 1:
                    playerPrefIndex = "SpellListTwo";
                    break;

                case 2:
                    playerPrefIndex = "SpellListThree";
                    break;

                case 3:
                    playerPrefIndex = "SpellListFour";
                    break;

                case 4:
                    playerPrefIndex = "SpellListFive";
                    break;

                default:
                    break;
            }

            int[] newSpellBar = PlayerPrefsX.GetIntArray(playerPrefIndex);
            Debug.Log(index[0] + " " + index[1]);
            newSpellBar[index[1]] =  RuneUI.itemBeingDragged.GetComponent<RuneUI>().GetSpell().GetId();

            PlayerPrefsX.SetIntArray(playerPrefIndex, newSpellBar);
        }

        #endregion

        #region Public Methods

        public void SetSpellId(int id)
        {
            spellId = id;
        }

        public int GetSpellId()
        {
            return spellId;
        }

        public void SetIndex(int bar, int pos)
        {
            index[0] = bar;
            index[1] = pos;
        }

        #endregion
    }
}
