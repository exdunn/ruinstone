using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace WizardWars
{
    public class ButtonHover : MonoBehaviour
        , IPointerEnterHandler
        , IPointerExitHandler
    {
        #region Public Variables

        /// <summary>
        /// Glow effect which surrounds the button when it is highlighted
        /// </summary>
        public GameObject glowEffect;

        #endregion

        #region EventSystems Handlers

        public void OnPointerEnter(PointerEventData data)
        {
            glowEffect.SetActive(true);
        }

        public void OnPointerExit(PointerEventData data)
        {
            glowEffect.SetActive(false);
        }

        #endregion
    }
}

