using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WizardWars
{
    public class GameManager : Photon.PunBehaviour {

        #region variables

        public GameObject playerPrefab;
        public GameObject gameMenu;
        public GameObject scoreboard;
        public Text displayMessage;
        public GameObject[] spawnPoints;

        public bool gameOver
        {
            get; set;
        }

        public int lives
        {
            get; set;
        }

        // dictionary that keeps track of players alive/dead
        // key => player ID
        // value => true = alive and false = dead
        Dictionary<int, bool> playerStatus;

        #endregion

        #region Public Methods

        /// <summary>
        /// Takes player back to main menu
        /// </summary>
        public void LeaveClick()
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene(0);
        }

        /// <summary>
        /// Close the in game menu
        /// </summary>
        public void CancelClick()
        {
            gameMenu.SetActive(false);
        }

        public void PlayerEliminated(int playerId)
        {
            // cannot find dying player
            if (!playerStatus.ContainsKey(playerId))
            {
                Debug.Log("Cannot find key " + playerId);
                return;
            }
    
            GetComponent<PhotonView>().RPC("BroadcastPlayerElimination", PhotonTargets.All, playerId);
        }

        #endregion

        #region private methods

        private void EndGame(int winner)
        {
            Debug.Log("end game");
            gameOver = true;
            displayMessage.text = "Player " + winner + " is victorious!";
            //PhotonNetwork.LeaveRoom();
            //PhotonNetwork.Disconnect();
        }

        #endregion

        #region monobehaviour callbacks

        // Use this for initialization
        void Start()
        {
            playerStatus = new Dictionary<int, bool>();

            // initialize all players to alive
            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                playerStatus[(int)player.CustomProperties["ID"]] = true;
            }

            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            }
            else
            {
                int index = System.Convert.ToInt32(PhotonNetwork.player.CustomProperties["ID"]);

                // spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                GameObject newPlayer = PhotonNetwork.Instantiate(this.playerPrefab.name, spawnPoints[index].transform.position, spawnPoints[index].transform.rotation, 0);
                newPlayer.GetComponent<PlayerControllerV2>().enabled = true;
                newPlayer.GetComponent<PlayerManager>().playerId = (int)PhotonNetwork.player.CustomProperties["ID"];
                lives = System.Convert.ToInt32(PhotonNetwork.room.CustomProperties["l"]);          
            }
        }

        // Update is called once per frame
        void Update()
        {
            // open game menu when player presses escape key
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!gameMenu.activeSelf)
                {
                    gameMenu.SetActive(true);
                }
                else
                {
                    gameMenu.SetActive(false);
                }
            }

            // open/close score board
            if (Input.GetKey(KeyCode.Tab))
            {
                scoreboard.GetComponent<CanvasGroup>().alpha = 1;
            }
            else if (Input.GetKeyUp(KeyCode.Tab))
            {
                scoreboard.GetComponent<CanvasGroup>().alpha = 0;
            }


            // ***** WIN CONDITION DETECTION *****

            // if only one player is left they are the winner
            if (playerStatus.Count(item => item.Value == true) == 1)
            {
                // for testing purposes only end the game if there is more than one person in the room
                if (PhotonNetwork.room.PlayerCount > 1)
                {
                    EndGame(playerStatus.FirstOrDefault(x => x.Value == true).Key);
                }
            }
            // less than one player remaining means the game is a draw
            else if (playerStatus.Count(item => item.Value == true) < 1)
            {
                Debug.Log("NO REMAINING PLAYERS, GAME IS A DRAW");
            }
        }

        void OnApplicationQuit()
        {
            Debug.Log("disconnecting from photon");
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }

        #endregion

        #region PUN RPC

        [PunRPC]
        public void BroadcastPlayerElimination(int playerId)
        {
            if (!playerStatus.ContainsKey(playerId))
            {
                Debug.LogError(playerId + " does not exist in playerStatus");
                return;
            }

            playerStatus[playerId] = false;
        }

        [PunRPC]
        public void UpdateSpawnPoint(int index)
        {
            spawnPoints[index].GetComponent<Spawn>().taken = true;
        }


        #endregion

        #region PUN callbacks

        public override void OnPhotonPlayerDisconnected(PhotonPlayer player)
        {
            Debug.Log("PLAYER " + player.CustomProperties["ID"] + "HAS DISCONNECTED");
        }

        #endregion

    }
}