using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace WizardWars
{
    public class AffinityUI : MonoBehaviour
        , IPointerEnterHandler
        , IPointerExitHandler
        , IPointerClickHandler
    {
        #region Public Variables

        /// <summary>
        /// tooltip which displays information and is opened when the user hovers over the panel
        /// </summary>
        public GameObject tooltip;

        /// <summary>
        /// Icon image which represents wizard's affinity
        /// </summary>
        public GameObject icon;

        #endregion

        #region Private Variables

        private Sprite[] sprites;

        private int affinityIndex;

        #endregion

        #region MonoBehaviour CallBacks

        void Start ()
        {
            affinityIndex = 0;

            sprites = new Sprite[3];
            sprites[0] = Resources.Load<UnityEngine.Sprite>("Sprites/UI/Spells/fireball_icon");
            sprites[2] = Resources.Load<UnityEngine.Sprite>("Sprites/UI/Spells/plasma_orb");
            sprites[1] = Resources.Load<UnityEngine.Sprite>("Sprites/UI/Spells/crackling_bolt_icon");
        }

        #endregion

        #region Public Methods

        public void OnPointerEnter(PointerEventData eventData)
        {
            tooltip.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tooltip.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            tooltip.GetComponent<Tooltip>().SetBody(GlobalVariable.AFFINITYTOOLTIPDESC[affinityIndex]);
            tooltip.GetComponent<Tooltip>().SetTitle(GlobalVariable.AFFINITYTOOLTIPTITLE[affinityIndex]);
            icon.GetComponent<Image>().sprite = sprites[affinityIndex];

            affinityIndex = (affinityIndex+1)%3;
        }

        #endregion
    }

}
