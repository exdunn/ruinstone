using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltraReal.SharedAssets.UnityStandardAssets;
using SpellSystem;

namespace WizardWars
{
    public class PlayerManager : Photon.PunBehaviour {

        #region Public Variables

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        // Leave variables public for now for testing purposes
        // Should be private in production version

        public float health = 100f;
        public float maxHealth = 100f;
        public int kills;
        public int deaths;
        public float damageDealt; 

        #endregion

        #region Private Variables

        AutoCam _autoCam;

        List<Status> _statuses;

        GameObject gameManager;
        GameObject message;
        ScoreboardManager scoreboard;
        GameObject[] spawnPoints;

        public int playerId
        {
            get; set;
        }

        public string playerName
        {
            get; set;
        }

        public int lives
        {
            get; set;
        }

        public int spawnIndex
        {
            get; set;
        }

        public bool pushed
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

            // initialize stats
            moveSpeedModifier = 1;
            damageModifier = 1;
            damageReceivedModifier = 1;
            cooldownReduction = 0;
        }

        void Start()
        {
            deaths = 0;
            kills = 0;
            dead = false;
            pushed = false;
            _statuses = new List<Status>();

            if (photonView.isMine)
            {
                GetComponent<PhotonView>().RPC("BroadcastPlayerId", PhotonTargets.All, playerId);
            }
  
            // set local gameManager
            gameManager = GameObject.Find("GameManager");
            lives = gameManager.GetComponent<GameManager>().lives;

            // set up spawn points
            spawnPoints = gameManager.GetComponent<GameManager>().spawnPoints;

            // set canvas message
            message = GameObject.Find("Canvas/Display Message");

            // set scoreboard
            scoreboard = gameManager.GetComponent<GameManager>().scoreboard.GetComponent<ScoreboardManager>();

            // set robe color
            GetComponent<PlayerControllerV2>().playerModel.GetComponent<Renderer>().materials[0].color = Color.red;

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

        #endregion

        #region Public Methods

        public void AddStatus(GameObject status) {
            //Create Instance of Status
            //Call Status's Apply
            //Add Status to List

            //status.GetComponent<PhotonView>().RPC("ReceivedAddStatus", PhotonTargets.All, status);
            status.GetComponent<Status>().Activate(this.gameObject, _statuses.Count);
            _statuses.Add(status.GetComponent<Status>());
        }

        public void RemoveStatus(int where) {
            //Call Status's Unapply
            //Remvoe Status from List
            _statuses[where].Deactivate(this.gameObject);
            _statuses.RemoveAt(where);
        }

        public void RemoveStatus(Status status)
        {
            status.Deactivate(this.gameObject);
            _statuses.Remove(status);
        }

        public void SetCrowdControl(int crowdControl, bool toggle) {
            //Toggle the specified crowd control to whatever
        }

        public void ForceMove(float magnitude) {

            //GetComponent<Rigidbody>().isKinematic = false;
            pushed = true;
            GetComponent<Rigidbody>().AddForce(-transform.forward * magnitude, ForceMode.Impulse);
        }

        public void Teleport(Vector3 point) {
            //Change the position of the player to point
        }

        /// <summary>
        /// Player death function
        /// </summary>
        public void PlayerDie()
        {
            dead = true;

            // increment deaths
            UpdateDeaths(1);

            // decrement lives
            UpdateLives(-1);

            // player death anim
            GetComponent<PhotonView>().RPC("ReceivedDyingAnim", PhotonTargets.All);

            // player is eliminated when they run out of lives
            if (lives <= 0)
            {
                if (photonView.isMine)
                {
                    message.GetComponentInChildren<UnityEngine.UI.Text>().text = "You have been eliminated!";
                    gameManager.GetComponent<GameManager>().PlayerEliminated(playerId);
                }
                return;
            }
            StartCoroutine(Respawn());
        }

        /// <summary>
        /// Update player's health by subtracting damage
        /// </summary>
        /// <param name="damage">Amount that is subtracted from ccurrent health</param>
        /// <returns>New health</returns>
        public float UpdateHealth(float damage)
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

            // broad cast player's new health to other players
            GetComponent<PhotonView>().RPC("ReceivedUpdateHealth", PhotonTargets.All, health);

            return health;   
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

            GetComponent<PhotonView>().RPC("BroadcastDeaths", PhotonTargets.All, deaths);
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

            // handle player death logic here
            if (health == 0)
            {
                PlayerDie();   
            }
        }

        // Play death animation
        [PunRPC]
        public void ReceivedRespawnAnim()
        {
            GetComponentInChildren<Animator>().SetTrigger("respawn");
        }

        // Broadcast kills to all players
        [PunRPC]
        public void BroadcastKills(int value)
        {
            kills = value;

            // update the scoreboard
            scoreboard.UpdateScoreLabelKills(playerId, kills);
        }

        // Broadcast deaths to all players
        [PunRPC]
        public void BroadcastDeaths(int value)
        {
            deaths = value;

            // update the scoreboard
            scoreboard.UpdateScoreLabelDeaths(playerId, deaths);
        }

        // Broadcast damage to all players
        [PunRPC]
        public void BroadcastDamageDealt(float value)
        {
            damageDealt = value;

            // update the scoreboard
            scoreboard.UpdateScoreLabelDamage(playerId, damageDealt);
        }

        // Set the player ID in everyone else's view
        [PunRPC]
        public void BroadcastPlayerId(int id)
        {
            playerId = id;
        }

        // Set moveSpeedModifier for remote player
        [PunRPC]
        public void UpdateMoveSpeedModifier(float value)
        {
            moveSpeedModifier = value;
        }

        // Set damageModifier for remote player
        [PunRPC]
        public void UpdateDamageModifier(float value)
        {
            damageModifier = value;
        }

        // Set damageReceivedModifier for remote player
        [PunRPC]
        public void UpdateDamageReceivedModifier(float value)
        {
            damageReceivedModifier = value;
        }

        // Set cooldownReduction for remote player
        [PunRPC]
        public void UpdateCooldownReduction(float value)
        {
            cooldownReduction = value;
        }

        // Teleport player to position
        [PunRPC]
        public void TeleportPlayer(Vector3 pos)
        {
            GetComponent<PlayerControllerV2>().newPosition = pos;
            transform.position = pos;
        }

        // Sends command to all clients in photon room
        // Make it so that the status is parented to the player in everyone's view
        [PunRPC]
        public void ReceivedAddStatus(GameObject status)
        {
            status.transform.parent = this.transform;
        }

        #endregion

        #region Private Methods

        IEnumerator Respawn()
        {
            if (message != null)
            {
                if (photonView.isMine)
                {
                    message.GetComponentInChildren<UnityEngine.UI.Text>().text = "Respawning...";
                }
            }
            
            yield return new WaitForSeconds(3f);

            // hide canvas message
            if (message != null)
            {
                if (photonView.isMine)
                {
                    message.GetComponentInChildren<UnityEngine.UI.Text>().text = "";
                }
            }

            // stop dying animation
            GetComponent<PhotonView>().RPC("ReceivedRespawnAnim", PhotonTargets.All);

            // restore health to max health
            health = maxHealth;

            // pick a random spawn location
            Transform spawnPoint = spawnPoints[playerId].transform;

            // teleport player to spawn point
            transform.position = spawnPoint.position;
            GetComponent<PlayerControllerV2>().newPosition = spawnPoint.position;
            GetComponent<PhotonView>().RPC("TeleportPlayer", PhotonTargets.All, spawnPoint.position);

            dead = false;
        }

        #endregion

    }
}

