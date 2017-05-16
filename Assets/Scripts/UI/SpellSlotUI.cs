using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace WizardWars
{
    public class SpellSlotUI : MonoBehaviour
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
            if (spriteHighlighted != null)
            {
                background.GetComponent<Image>().sprite = spriteNormal;
                background.GetComponent<Image>().SetNativeSize();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (spriteHighlighted != null)
            {
                background.GetComponent<Image>().sprite = spriteHighlighted;
                background.GetComponent<Image>().SetNativeSize();
            }

            UpdateTooltip();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (RuneUI.itemBeingDragged == null)
            {
                return;
            }

            spellIcon.GetComponent<Image>().sprite = RuneUI.itemBeingDragged.GetComponent<RuneUI>().GetSpell().iconSprite;
            spellId = RuneUI.itemBeingDragged.GetComponent<RuneUI>().GetSpell().id;
            UpdateTooltip();

            string playerPrefIndex = "";

            switch (index[0])
            {
                case 1:
                    playerPrefIndex = "Preset1";
                    break;

                case 2:
                    playerPrefIndex = "Preset2";
                    break;

                case 3:
                    playerPrefIndex = "Preset3";
                    break;

                case 4:
                    playerPrefIndex = "Preset4";
                    break;

                default:
                    break;
            }

            int[] newSpellBar = PlayerPrefsX.GetIntArray(playerPrefIndex);
            Debug.Log(index[0] + " " + index[1]);
            newSpellBar[index[1]] =  RuneUI.itemBeingDragged.GetComponent<RuneUI>().GetSpell().id;

            PlayerPrefsX.SetIntArray(playerPrefIndex, newSpellBar);
            GetComponentInParent<AthenaeumManager>().UpdateRunesIsDraggable();
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

        #region Private Methods 

        /// <summary>
        /// Updates the tooltip with current spell ID
        /// </summary>
        private void UpdateTooltip()
        {
            if (!GetComponentInParent<AthenaeumManager>())
            {
                return;
            }

            // Search spell library for spell with spellId and use it to set the tooltip
            GetComponentInParent<AthenaeumManager>().tooltip.GetComponent<Tooltip>().ParseSpellStats(GameObject.FindGameObjectWithTag("Library").GetComponents<SpellStats>()[spellId]);
        }

        #endregion
    }
}
