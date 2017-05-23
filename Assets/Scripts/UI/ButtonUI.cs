using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace WizardWars
{
    public class ButtonUI : MonoBehaviour
        , IPointerClickHandler
    {

        #region Private Variables

        /// <summary>
        /// text printed on the button
        /// </summary>
        public GameObject buttonText;

        #endregion

        #region MonoBehaviour Callbacks

        // Use this for initialization
        void Start()
        {

        }

        #endregion

        #region Public Methods

        public void SetText(string input)
        {
            buttonText.GetComponent<Text>().text = input;
        }

        #endregion

        #region events

        public void OnPointerClick(PointerEventData eventData)
        {
            //GetComponent<AudioSource>().Play();
        }

        #endregion
    }

}
