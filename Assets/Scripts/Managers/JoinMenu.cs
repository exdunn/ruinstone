using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace WizardWars
{
    public class JoinMenu : Photon.PunBehaviour
    {

        #region Public Variables

        /// <summary>
        /// The PUN loglevel. 
        /// </summary>
        public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;

        [Tooltip("Holds the location for where to draw the game name labels")]
        public Text roomListLabel;

        [Tooltip("Label prefab that displays information about the game")]
        public GameObject lobbyPrefab;

        [Tooltip("Panel which is used as the parent when instantiating lobby labels")]
        public GameObject gameListPanel;

        [Tooltip("The pop up window that displays status messages to the player")]
        public GameObject popUpWindow;

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
        public string roomName
        {
            get; set;
        }

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

        public void LobbyLabelClick(int index, string roomName)
        {
            Image im;
            foreach (Transform child in gameListPanel.transform)
            {
                im = child.gameObject.GetComponent<Image>();
                im.color = new Color32(255, 255, 255, 100);
            }

            im = gameListPanel.transform.GetChild(index).GetComponent<Image>();
            im.color = new Color32(255, 255, 255, 255);
            this.roomName = roomName;
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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Main Menu");
            }
        }

        #endregion

        #region Photon CallBacks

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to Master");
            PhotonNetwork.JoinLobby();
        }

        public override void OnReceivedRoomListUpdate()
        {
            // get list of existing PUN rooms
            RoomInfo[] roomList = PhotonNetwork.GetRoomList();

            // destroy existing lobby labels
            foreach (Transform child in gameListPanel.transform)
            {
                Destroy(child.gameObject);
            }

            // instantiate player label for each room in roomList
            foreach (RoomInfo room in roomList)
            {
                if (room.IsOpen)
                {
                    Transform parentTransform = gameListPanel.transform;
                    GameObject newLobbyLabel = Instantiate(lobbyPrefab, new Vector3(0, 0, 0), parentTransform.rotation, parentTransform);
                    newLobbyLabel.GetComponent<LobbyLabel>().SetNameLabel(room.Name);
                    newLobbyLabel.GetComponent<LobbyLabel>().SetPlayersLabel(room.PlayerCount + "/" + room.MaxPlayers + " Players");
                }
            }
        }

        public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
        {
            popUpWindow.GetComponent<PopUpWindow>().Show(codeAndMsg[1].ToString());
        }

        #endregion

    }
}

