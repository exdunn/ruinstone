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
        Boolean isDraggable = true;

        #endregion

        #region MonoBehaviour Callbacks

        void Start()
        {
            unmaskedParent = GameObject.FindGameObjectWithTag("Rune Panel").transform;
            startParent = transform.parent;
        }      

        public void OnPointerEnter(PointerEventData eventData)
        {
            runeImage.GetComponent<Image>().sprite = spell.highlightedRuneSprite;
            GetComponentInParent<CollectionManager>().tooltip.GetComponent<Tooltip>().ParseSpellStats(spell);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            runeImage.GetComponent<Image>().sprite = spell.runeSprite;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!isDraggable)
            {
                return;
            }

            itemBeingDragged = gameObject;

            runeImage.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 1);

            // remember the objects starting parent
            startParent = transform.parent;

            // set block raycast to false so that we can drop in another panel
            GetComponent<CanvasGroup>().blocksRaycasts = false;

            // set parent to unmasked panel so the dragged object isn't masked
            transform.parent = unmaskedParent.transform;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isDraggable)
            {
                return;
            }

            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            itemBeingDragged = null;

            runeImage.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            // reparent the dragged object to its starting parent
            transform.parent = startParent;

            // grid layout uses the objects index to position it in the grid
            GetComponent<RectTransform>().SetSiblingIndex(spell.id);

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

        public void SetIsDraggable(Boolean input)
        {
            // set rune color based on if it is dragable
            runeImage.GetComponent<Image>().color = input ? Color.white : Color.gray;
            isDraggable = input;
        }

        #endregion
    }
}

