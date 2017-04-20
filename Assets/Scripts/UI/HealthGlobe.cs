using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace WizardWars
{
    public class HealthGlobe : MonoBehaviour
        , IPointerEnterHandler
        , IPointerExitHandler
    {
        #region Private Variables

        /// <summary>
        /// Tooltip that shows current health out of max health and displays when the user hovers over health globe
        /// </summary>
        GameObject tooltip;

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
            tooltip = transform.GetChild(1).gameObject;
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

