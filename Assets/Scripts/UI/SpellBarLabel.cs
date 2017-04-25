using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace WizardWars
{
    public class SpellBarLabel : MonoBehaviour
        , IPointerEnterHandler
        , IPointerExitHandler
        , IPointerClickHandler
    {
        #region Public Variables

        public GameObject text;

        #endregion


        #region Private Variables

        /// <summary>
        /// Index of the current spell bar
        /// </summary>
        int index = 0;

        #endregion

        #region MonoBehaviour Callbacks

        // Use this for initialization
        void Start()
        {
            text.GetComponent<Text>().text = PlayerPrefsX.GetStringArray("SpellBarNames")[index];
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (index < GlobalVariable.DECKCOUNT - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
            text.GetComponent<Text>().text = PlayerPrefsX.GetStringArray("SpellBarNames")[index];
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            text.GetComponent<Text>().color = Color.red;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            text.GetComponent<Text>().color = Color.black;
        }

        #endregion

    }
}
