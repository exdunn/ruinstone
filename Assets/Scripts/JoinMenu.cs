using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WizardWars
{
    public class JoinMenu : Photon.PunBehaviour
    {

        #region Public Variables

        /// <summary>
        /// Holds the location for where to draw the game name labels
        /// </summary>
        public Text roomListLabel;

        /// <summary>
        /// Holds the location for where to draw the game name labels
        /// </summary>
        public GameObject labelAnchor;

        /// <summary>
        /// The PUN loglevel. 
        /// </summary>
        public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;

        /// <summary>
        /// Lobby label prefab
        /// </summary>
        public GameObject lobbyPrefab;

        /// <summary>
        /// Panel which is used as the parent when instantiating lobby labels
        /// </summary>
        public GameObject gameListPanel;

        #endregion

        #region Private Variables

        /// <summary>
        /// Font style for Wizard Wars menus
        /// </summary>
        GUIStyle menuStyle;

        /// <summary>
        /// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
        /// </summary>
        string _gameVersion = "1";

        /// <summary>
        /// Name of the selected room.
        /// Default is an empty string
        /// </summary>
        string roomName;

        #endregion

        #region Public Methods

        /// <summary>
        /// Join the room corresponding to the highlighted lobby label
        /// Load the lobby scene corresponding to the room name
        /// </summary>
        public void JoinClick()
        {
            // join the room if 
            if (!roomName.Equals(""))
            {
                PhotonNetwork.JoinRoom(roomName);
            }
        }

        /// <summary>
        /// Set roomName to value
        /// </summary>
        public void SetRoomName(string value)
        {
            roomName = value;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        void Awake()
        {
            menuStyle = new GUIStyle();
            menuStyle.alignment = TextAnchor.UpperCenter;
            menuStyle.fontSize = 20;

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
            PhotonNetwork.ConnectUsingSettings(_gameVersion);
            roomName = "";
        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion

        #region Photon.PunBehaviour CallBacks

        public override void OnConnectedToPhoton()
        {
            Debug.Log("Connected to Photon");
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to Master");
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("Joined lobby");
        }

        public override void OnReceivedRoomListUpdate()
        {
            // get list of existing PUN rooms
            RoomInfo[] roomList = PhotonNetwork.GetRoomList();
            roomListLabel.text = "Number of Rooms: " + roomList.Length;
            Debug.Log("Number of Rooms: " + roomList.Length);

            // instantiate lobby label for each room in roomList
            int i = 1;
            foreach (RoomInfo room in roomList)
            {
                Transform parentTransform = gameListPanel.transform;
                Vector3 pos = new Vector3(parentTransform.position.x,
                    parentTransform.position.y + 225 - 100 * i,
                    parentTransform.position.z);
                i++;
                GameObject newLobbyLabel = Instantiate(lobbyPrefab, pos, parentTransform.rotation, parentTransform);
                newLobbyLabel.GetComponent<LobbyLabel>().SetNameLabel(room.Name);
            }
        }

        #endregion

    }
}

