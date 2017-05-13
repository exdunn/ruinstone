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

        // dictionary that keeps track of players alive/dead
        // key => player ID
        // value => true = alive and false = dead
        Dictionary<int, bool> playerStatus;

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
            // cannot find dying player
            if (!playerStatus.ContainsKey(playerId))
            {
                Debug.Log("Cannot find key " + playerId);
                return;
            }

            playerStatus[playerId] = false;
            Debug.Log("PLAYER " + playerId + " HAS DIED");
            //GetComponent<PhotonView>().RPC("ReceivedPlayerDeath", PhotonTargets.All, playerId);

            Debug.Log("number of remaining players: " + playerStatus.Count(c => true));

            // if there is one remaining player alive game is over
            // surviving player is declared the winner
            if (playerStatus.Count(item => item.Value == true) == 1)
            {
                int winner = playerStatus.FirstOrDefault(x => x.Value == true).Key;
                Debug.Log("THE WINNER IS PLAYER " + winner);
            }
            // less than one player remaining means the game is a draw
            else if (playerStatus.Count(item => item.Value == true) < 1)
            {
                Debug.Log("NO REMAINING PLAYERS, GAME IS A DRAW");
            }
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
                // pick a random spawn location
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform;

                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                GameObject newPlayer = PhotonNetwork.Instantiate(this.playerPrefab.name, spawnPoint.position, spawnPoint.rotation, 0);
                newPlayer.GetComponent<PlayerControllerV2>().enabled = true;
                newPlayer.GetComponent<PlayerManager>().playerId = (int)PhotonNetwork.player.CustomProperties["ID"];
                
                // Debug.Log("PLAYER ID: " + (int)PhotonNetwork.player.CustomProperties["ID"]);
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
        }

        #endregion

        #region PUN RPC

        [PunRPC]
        public void ReceivedPlayerDeath(int playerId)
        {
            if (!playerStatus.ContainsKey(playerId))
            {
                Debug.LogError(playerId + " does not exist in playerStatus");
                return;
            }

            playerStatus[playerId] = false;
            Debug.Log("PLAYER " + playerId + " HAS DIED");
        }


        #endregion
   
    }
}