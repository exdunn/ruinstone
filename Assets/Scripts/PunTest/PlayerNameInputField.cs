using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PunTest
{
    /// <summary>
    /// Player name input field. Let the user input his name, will appear above the player in the game.
    /// </summary>
    [RequireComponent(typeof(PlayerNameInputField))]
    public class PlayerNameInputField : MonoBehaviour
    {

        static string playerNamePrefKey = "PlayerName";

        // Use this for initialization
        void Start()
        {
            string defaultName = "";
            InputField _inputField = this.GetComponent<InputField>();
            if (_inputField != null)
            {
                if (PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    _inputField.text = defaultName;
                }
            }
            PhotonNetwork.playerName = defaultName;
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Sets the name of the player, and save it in the PlayerPrefs for future sessions.
        /// </summary>
        /// <param name="value">The name of the Player</param>
        public void SetPlayerName(string value)
        {
            PhotonNetwork.playerName = value + " ";

            PlayerPrefs.SetString(playerNamePrefKey, value);
        }
    }
}


