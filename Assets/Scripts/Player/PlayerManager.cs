using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltraReal.SharedAssets.UnityStandardAssets;

namespace WizardWars
{
    public class PlayerManager : Photon.PunBehaviour {

        #region Public Variables

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        // Leave variables public for now for testing purposes
        // Should be private in production version

        [Tooltip("The current Health of our player")]
        public float health = 100f;

        [Tooltip("The maximum Health of our player")]
        public float maxHealth = 100f;

        [Tooltip("The number of kills player has this round")]
        public int kills;

        [Tooltip("The number of times the player has died this round")]
        public int deaths;   

        #endregion

        #region Private Variables

        AutoCam _autoCam;

        SpellStats[] library;

        GameObject gameManager;
        GameObject playerUI;
        GameObject[] spawnPoints;

        public int playerId
        {
            get; set;
        }

        public int lives
        {
            get; set;
        }

        #endregion

        #region player stats

        public float moveSpeedModifier
        {
            get;set;
        }

        public float damageModifier
        {
            get;set;
        }

        public float damageReceivedModifier
        {
            get;set;
        }

        public float cooldownReduction
        {
            get;set;
        }

        public bool dead
        {
            get; set;
        }

        #endregion

        #region MonoBehaviour Callbacks

        void Awake()
        {
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.isMine)
            {
                LocalPlayerInstance = this.gameObject;
            }

            // set all stats to 1
            moveSpeedModifier = 1;
            damageModifier = 1;
            damageReceivedModifier = 1;
            cooldownReduction = 1;

            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            // DontDestroyOnLoad(this.gameObject);
        }

        void Start()
        {
            deaths = 0;
            kills = 0;
            dead = false;

            if (photonView.isMine)
            {
                GetComponent<PhotonView>().RPC("BroadcastPlayerId", PhotonTargets.All, playerId);
            }

            // set up spawn points
            spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");

            // set local gameManager
            gameManager = GameObject.Find("GameManager");

            // attach camera to player
            _autoCam = Camera.main.GetComponentInParent<AutoCam>();

            if (photonView.isMine)
            {
                if (_autoCam.GetComponent<AutoCam>() != null)
                {
                    _autoCam.GetComponent<AutoCam>().SetTarget(transform);
                }
                else
                {
                    Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
                }

                // set local playerUI
                GameObject.Find("Canvas/PlayerUI").GetComponent<PlayerUI>().SetTarget(this);
            }
        }

        void Update()
        {
            

            //Check the list for finished statuses
            //For each finished status, call Remove Status on it
        } 

        #endregion

        #region Public Methods

        public void AddStatus(int status) {
            //Create Instance of Status
            //Call Status's Apply
            //Add Status to List
        }

        public void RemoveStatus(int status) {
            //Call Status's Unapply
            //Remvoe Status from List
        }

        public void SetCrowdControl(int crowdControl, bool toggle) {
            //Toggle the specified crowd control to whatever
        }

        public void ForceMove(float magnitude, Vector3 direction) {
            //Push the player in some direction
        }

        public void Teleport(Vector3 point) {
            //Change the position of the player to point
        }

        public void Respawn()
        {
            if (lives <= 0)
            {
                Debug.Log("Player has no more lives");
                return;
            }
            StartCoroutine(RespawnTimer());
        }

        /// <summary>
        /// Update current health of the player
        /// </summary>
        /// <param name="damage"></param>
        public void UpdateHealth(float damage)
        {
            health += damage; 

            if (health >= maxHealth)
            {
                health = maxHealth;
            }
            else if (health < 0)
            {
                health = 0;
            }

            GetComponent<PhotonView>().RPC("ReceivedUpdateHealth", PhotonTargets.All, health);    
        }

        /// <summary>
        /// update number of kills player has
        /// </summary>
        /// <param name="update"></param>
        public void UpdateKills (int update)
        {
            kills += update;

            if (kills < 0)
            {
                kills = 0;
            }
        }

        /// <summary>
        /// update number of kills player has
        /// </summary>
        /// <param name="update"></param>
        public void UpdateLives(int update)
        {
            lives += update;

            if (lives < 0)
            {
                lives = 0;
            }

            if (lives == 0)
            {
                // tell game manager player has no more lives
            }
        }

        /// <summary>
        /// update number of deaths player has
        /// </summary>
        /// <param name="update"></param>
        public void UpdateDeaths(int update)
        {
            deaths += update;

            if (deaths < 0)
            {
                deaths = 0;
            }
        }

        /// <summary>
        /// Return number of kills
        /// </summary>
        public int GetKills ()
        {
            return kills;
        }

        /// <summary>
        /// Return number of deaths
        /// </summary>
        public int GetDeaths()
        {
            return deaths;
        }

        /// <summary>
        /// Return current heatlh
        /// </summary>
        public float GetHealth()
        {
            return health;
        }

        #endregion

        #region PUN RPC

        /// <summary>
        /// Update photon view of player's health
        /// </summary>
        /// <param name="curHealth"></param>
        [PunRPC]
        public void ReceivedUpdateHealth(float newHealth)
        {
            health = newHealth;

            if (health == 0)
            {
                Debug.Log("player has died!");

                dead = true;

                // player death anim
                GetComponent<PhotonView>().RPC("ReceivedDieAnim", PhotonTargets.All, true);

                // respawn player
                Respawn();

                // tell game manager player is dead
                gameManager.GetComponent<GameManager>().PlayerDie(playerId);
            }
        }

        // Play death animation
        [PunRPC]
        public void ReceivedDieAnim(bool die)
        {
            GetComponent<Animator>().SetBool("dead", die);
        }

        // Set the player ID in everyone else's view
        [PunRPC]
        public void BroadcastPlayerId(int id)
        {
            Debug.Log("broadcastplayerid called");
            playerId = id;
        }

        // Teleport player to position
        [PunRPC]
        public void TeleportPlayer(Vector3 pos)
        {
            GetComponent<PlayerControllerV2>().newPosition = pos;
            transform.position = pos;
        }

        #endregion

        #region Private Methods

        IEnumerator RespawnTimer()
        {
            yield return new WaitForSeconds(3f);
            
            // pick a random spawn location
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform;

            // teleport player to spawn point
            transform.position = spawnPoint.position;
            GetComponent<PhotonView>().RPC("TeleportPlayer", PhotonTargets.All, spawnPoint.position);

            dead = false;
        }

        #endregion

    }
}

