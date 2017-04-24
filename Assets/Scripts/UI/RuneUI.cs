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
        , IBeginDragHandler
        , IDragHandler
        , IEndDragHandler
    {
        #region Public Variables

        public static GameObject itemBeingDragged;
        public GameObject runeImage;

        #endregion

        #region Private Variables

        SpellStats spell;
        Transform unmaskedParent;
        Transform startParent;

        #endregion

        #region MonoBehaviour Callbacks

        public void Start()
        {
            unmaskedParent = GameObject.FindGameObjectWithTag("Rune Panel").transform;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            runeImage.GetComponent<Image>().sprite = spell.GetHighlightedRuneSprite();
            GetComponentInParent<CollectionManager>().UpdateTooltip(spell);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            runeImage.GetComponent<Image>().sprite = spell.GetRuneSprite();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            itemBeingDragged = gameObject;

            // remember the objects starting parent
            startParent = transform.parent;

            // set block raycast to false so that we can drop in another panel
            GetComponent<CanvasGroup>().blocksRaycasts = false;

            // set parent to unmasked panel so the dragged object isn't masked
            transform.parent = unmaskedParent.transform;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            itemBeingDragged = null;

            // reparent the dragged object to its starting parent
            transform.parent = startParent;

            // grid layout uses the objects index to position it in the grid
            GetComponent<RectTransform>().SetSiblingIndex(spell.GetId());

            // set block raycast to true
            GetComponent<CanvasGroup>().blocksRaycasts = true;
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

