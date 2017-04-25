using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class HighlightOnMouseOver : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{

    public Sprite spriteNormal;
    public Sprite spriteHighlighted;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = spriteHighlighted;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = spriteNormal;
    }
}
