using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace WizardWars
{
    public class RuneUI : MonoBehaviour
        , IPointerEnterHandler
        , IPointerExitHandler
    {
        #region Public Variables

        public GameObject runeImage;

        #endregion

        #region Private Variables

        SpellStats spell;

        #endregion

        #region MonoBehaviour Callbacks

        public void OnPointerEnter(PointerEventData eventData)
        {
            runeImage.GetComponent<Image>().sprite = spell.GetHighlightedRuneSprite();
            GetComponentInParent<RuneGrid>().UpdateTooltip(spell);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            runeImage.GetComponent<Image>().sprite = spell.GetRuneSprite();
        }

        #endregion


        #region Public Methods

        public void SetSpell(SpellStats value)
        {
            spell = value;
        }

        public SpellStats GetSpell()
        {
            return spell;
        }

        #endregion
    }
}

