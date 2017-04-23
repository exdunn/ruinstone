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
    {

        #region Public Variables

        public Sprite spriteNormal;
        public Sprite spriteHighlighted;
        public GameObject spellIcon;
        public GameObject background;

        #endregion

        #region Private Variables

        #endregion

        // Use this for initialization
        void Start()
        {
       
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            background.GetComponent<Image>().sprite = spriteNormal;
            background.GetComponent<Image>().SetNativeSize();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            background.GetComponent<Image>().sprite = spriteHighlighted;
            background.GetComponent<Image>().SetNativeSize();
        }
    }

}
