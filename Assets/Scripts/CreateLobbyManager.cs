﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WizardWars
{
    public class CreateLobbyManager : Photon.PunBehaviour
    {
        #region Public Variables

        /// <summary>
        /// The PUN loglevel. 
        /// </summary>
        public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;

        [Tooltip("The Ui Panel to let the user enter game name, connect and play")]
        public GameObject controlPanel;
        [Tooltip("The UI Label to inform the user that the connection is in progress")]
        public GameObject progressLabel;

        /// <summary>
        /// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
        /// </summary>   
        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        public byte MaxPlayersPerRoom = 4;

        #endregion

        #region Private Variables

        /// <summary>
        /// Input field for game name
        /// </summary>
        InputField nameInputField;

        /// <summary>
        /// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
        /// </summary>
        string _gameVersion = "1";

        /// <summary>
        /// The name given to the created PUN Network lobby
        /// </summary>
        string gameName = "Default";

        /// <summary>
        /// Keep track of the current process. Since connection is asynchronous and is based on several callbacks from Photon, 
        /// we need to keep track of this to properly adjust the behavior when we receive call back by Photon.
        /// Typically this is used for the OnConnectedToMaster() callback.
        /// </summary>
        bool isConnecting;

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a PUN lobby with the user-input name
        /// </summary>
        public void Create()
        {
            isConnecting = true;

            progressLabel.SetActive(true);
            controlPanel.SetActive(false);

            if (!PhotonNetwork.connected)
            {
                PhotonNetwork.ConnectUsingSettings(_gameVersion);
            }
            else
            {
                Debug.Log("Photon is already connected... creating new room");
                Debug.Log("game name: " + gameName);
                PhotonNetwork.CreateRoom(gameName, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
            }
            // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
            /*if (PhotonNetwork.connected)
            {
                Debug.Log(gameName);
                PhotonNetwork.CreateRoom(gameName, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
            }
            else {
                // #Critical, we must first and foremost connect to Photon Online Server.
                PhotonNetwork.ConnectUsingSettings(_gameVersion);
            }*/            
        }

        /// <summary>
        /// Sets private variable gameName to the value of the game name input field
        /// </summary>
        public void SetGameName(string value)
        {
            gameName = value;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        void Awake()
        {
            // #NotImportant
            // Force Full LogLevel
            PhotonNetwork.logLevel = Loglevel;

            // #Critical
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.automaticallySyncScene = true;
        }

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        void Start()
        {
            nameInputField = GetComponent<InputField>();

            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }

        #endregion

        #region Photon.PunBehaviour CallBacks

        public override void OnJoinedLobby()
        {
            Debug.Log("DemoAnimator / Launcher: OnJoinedLobby() was called by PUN");
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");
            PhotonNetwork.JoinLobby();
            // we don't want to do anything if we are not attempting to join a room. 
            // this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
            // we don't want to do anything.
            if (isConnecting)
            {
                Debug.Log("game name: " + gameName);
                PhotonNetwork.CreateRoom(gameName, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
            }
        }

        public override void OnDisconnectedFromPhoton()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);

            Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            Debug.Log("DemoAnimator/Launcher:OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");

            PhotonNetwork.LoadLevel("Lobby");
        }


        #endregion

    }
}