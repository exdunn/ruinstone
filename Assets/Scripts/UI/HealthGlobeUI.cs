﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace WizardWars
{
    public class HealthGlobeUI : MonoBehaviour
        , IPointerEnterHandler
        , IPointerExitHandler
    {
        #region Private Variables

        /// <summary>
        /// Tooltip that shows current health out of max health and displays when the user hovers over health globe
        /// </summary>
        public GameObject tooltip;

        /// <summary>
        /// empty health globe that fills up as the user takes damage
        /// </summary>
        GameObject fill;

        #endregion

        #region MonoBehaviour CallBacks

        // Use this for initialization
        void Start()
        {
            fill = transform.GetChild(0).gameObject;
        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Set fill ammount of the empty health globe
        /// </summary>
        /// <param name="ratio"></param>
        public void SetFill(float ratio)
        {
            fill.GetComponent<Image>().fillAmount = ratio;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            tooltip.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tooltip.SetActive(false);
        }

        #endregion

    }
}

