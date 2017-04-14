using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace WizardWars
{
    public class LobbyLabel : MonoBehaviour
        , IPointerClickHandler
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

        /// <summary>
        /// Text element that displays the game mode
        /// </summary>
        public Text modeLabel;

        #endregion

        #region Private Variables

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
        /// Set modeLabel to given string
        /// </summary>
        public void SetModeLabel(string value)
        {
            modeLabel.text = value;
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

        /// <summary>
        /// Return string value of modeLabel
        /// </summary>
        public string GetModeLabel()
        {
            return modeLabel.text;
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
            GameObject joinMenu = GameObject.FindGameObjectWithTag("Join Menu");
            Debug.Log("room name: " + GetComponent<LobbyLabel>().GetNameLabel());
            joinMenu.GetComponent<JoinMenu>().SetRoomName(GetComponent<LobbyLabel>().GetNameLabel());

            Image im = GetComponent<Image>();
            im.color = new Color32((byte)im.color.r, (byte)im.color.g, (byte)im.color.b, 100);
        }

        #endregion

    }
}
