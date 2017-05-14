using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace WizardWars
{
    public class LobbyLabel : MonoBehaviour
        , IPointerClickHandler
        , IPointerEnterHandler
        , IPointerExitHandler
    {
        #region Public Variables

        /// <summary>
        /// Text element that displays the game name 
        /// </summary>
        public Text nameLabel;

        /// <summary>
        /// Text element that displays the number of players out of the maximum number of players 
        /// </summary>
        public Text playersLabel;

        #endregion

        #region Private Variables

        public bool clicked
        {
            get; set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Set nameLabel to given string
        /// </summary>
        public void SetNameLabel (string value)
        {
            nameLabel.text = value;
        }

        /// <summary>
        /// Set playersLabel to given string
        /// </summary>
        public void SetPlayersLabel(string value)
        {
            playersLabel.text = value;
        }

        /// <summary>
        /// Return string value of nameLabel
        /// </summary>
        public string GetNameLabel()
        {
            return nameLabel.text;
        }

        /// <summary>
        /// Return string value of playersLabel
        /// </summary>
        public string GetPlayersLabel()
        {
            return playersLabel.text;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion

        #region EventSystems Handlers

        public void OnPointerClick(PointerEventData data)
        {
            JoinMenu joinMenu = GameObject.Find("Join Menu").GetComponent<JoinMenu>();
            joinMenu.LobbyLabelClick(transform.GetSiblingIndex(), nameLabel.text);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (clicked)
            {
                return;
            }

            GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            GetComponent<Image>().color = new Color32(255, 255, 255, 200);
        }

        #endregion

    }
}
