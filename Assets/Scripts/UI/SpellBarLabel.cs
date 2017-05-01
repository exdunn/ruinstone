using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace WizardWars
{
    public class SpellBarLabel : Photon.MonoBehaviour
        , IPointerEnterHandler
        , IPointerExitHandler
        , IPointerClickHandler
    {
        #region Public Variables

        Text text;

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
            text = GetComponentInChildren<Text>();
            text.text = PlayerPrefsX.GetStringArray("SpellBarNames")[0];
            photonView.RPC("PhotonLabelChange", PhotonTargets.All, text.text);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("id: " + PhotonNetwork.player.ID);
            Debug.Log("parent id: " + GetComponentInParent<PlayerLabel>().playerId);
            Debug.Log("Photon view: " + photonView.instantiationId);

            if (PhotonNetwork.player.ID != GetComponentInParent<PlayerLabel>().playerId)
            {
                return;
            }
            if (index < GlobalVariable.DECKCOUNT - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
            text.text = PlayerPrefsX.GetStringArray("SpellBarNames")[index];
            photonView.RPC("PhotonLabelChange", PhotonTargets.All, text.text);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            text.GetComponent<Text>().color = Color.red;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            text.GetComponent<Text>().color = Color.black;
        }

        [PunRPC]
        public void PhotonLabelChange (string value)
        {
            
            text.text = value;
        }

        #endregion

    }
}
