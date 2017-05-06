using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WizardWars
{
    public class GameManager : MonoBehaviour {

        #region Public Variables

        [Tooltip("The prefab to use for representing the player")]
        public GameObject playerPrefab;

        /// <summary>
        /// Game menu which can be opened by pressing ESC
        /// </summary>
        public GameObject gameMenu;

        public GameObject myText;

        #endregion

        #region Private Variables

        /// <summary>
        /// true if the game is over, false if not over
        /// </summary>
        public bool gameOver
        {
            get; set;
        }


        /// <summary>
        /// Status of players
        /// If true then the player at index i is alive
        /// If false then the player at index i is dead
        /// </summary>
        public bool[] playerStatus
        {
            get; set;
        }

        public GameObject[] spawnPoints;

        #endregion

        #region Public Methods

    /// <summary>
    /// Takes player back to main menu
    /// </summary>
    public void LeaveClick()
        {
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

        public void PlayerDie(int playerId)
        {
            if (playerId < 0 || playerId >= playerStatus.Length)
            {
                Debug.Log("PLAYER INDEX " + playerId + " OUT OF RANGE");
            }

            playerStatus[playerId] = false;
            Debug.Log("PLAYER " + playerId + "HAS DIED");
            //GetComponent<PhotonView>().RPC("ReceivedPlayerDeath", PhotonTargets.All, playerId);
        }

        #endregion

        #region monobehaviour callbacks

        // Use this for initialization
        void Start()
        {
            // initialize all players to alive
            playerStatus = new bool[PhotonNetwork.room.PlayerCount];
            for (int i = 0; i < playerStatus.Length; i++)
            {
                playerStatus[i] = true;
            }

            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            }
            else
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                GameObject newPlayer = PhotonNetwork.Instantiate(this.playerPrefab.name, spawnPoint.position, spawnPoint.rotation, 0);
                newPlayer.GetComponent<PlayerManager>().playerId = PhotonNetwork.player.ID;
                newPlayer.GetComponent<PlayerControllerV2>().enabled = true;
                //Debug.Log("player id: " + PhotonNetwork.player.ID);
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

            Debug.Log("number of remaining players: " + playerStatus.Count(c => true));

            // if there is one remaining player alive game is over
            // surviving player is declared the winner
            if (playerStatus.Count(c => true) == 1)
            {
                int winner = System.Array.IndexOf(playerStatus, true);
                Debug.Log("THE WINNER IS PLAYER " + winner);
            }
            // less than one player remaining means the game is a draw
            else if (playerStatus.Count(c => true) < 1)
            {
                Debug.Log("NO REMAINING PLAYERS, GAME IS A DRAW");
            }
        }

        #endregion

        #region PUN RPC

        [PunRPC]
        public void ReceivedPlayerDeath(int playerId)
        {
            if (playerId < 0 || playerId >= playerStatus.Length)
            {
                Debug.Log("PLAYER INDEX " + playerId + " OUT OF RANGE");
            }

            playerStatus[playerId] = false;
            Debug.Log("PLAYER " + playerId + "HAS DIED");
        }


        #endregion
   
    }
}