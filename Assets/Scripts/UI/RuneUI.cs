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

        public Sprite spriteNormal;
        public Sprite spriteHighlighted;
        public GameObject runeImage;

        #endregion

        #region MonoBehaviour Callbacks

        public void OnPointerEnter(PointerEventData eventData)
        {
            runeImage.GetComponent<Image>().sprite = spriteHighlighted;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            runeImage.GetComponent<Image>().sprite = spriteNormal;
        }

        #endregion

    }
}

