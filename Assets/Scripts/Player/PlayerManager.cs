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

        #endregion

        #region Private Variables

        AutoCam _autoCam;

        List<Status> _statuses;

        GameObject gameManager;
        GameObject message;
        GameObject[] spawnPoints;

        public int playerId
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

            // set all stats to 0
            moveSpeedModifier = 1;
            damageModifier = 1;
            damageReceivedModifier = 1;
            cooldownReduction = 0;

            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            // DontDestroyOnLoad(this.gameObject);
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

            // set up spawn points
            spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");

            // set local gameManager
            gameManager = GameObject.Find("GameManager");
            lives = gameManager.GetComponent<GameManager>().lives;

            // set canvas message
            message = GameObject.Find("Canvas/Display Message");
            Debug.Log("MESSAGE " + message);

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
            // damage taken is damage * player's damage received modifier
            float finalDamage = damage * damageReceivedModifier;
            health += finalDamage; 

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
                if (lives == 0)
                {
                    gameManager.GetComponent<GameManager>().PlayerEliminated(playerId);
                }
                
            }
        }

        // Play death animation
        [PunRPC]
        public void ReceivedDieAnim(bool die)
        {
            GetComponentInChildren<Animator>().SetBool("dead", die);
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
        public void UpdateCooldownRedution(float value)
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

        IEnumerator RespawnTimer()
        {
            if (message != null)
            {
                if (photonView.isMine)
                {
                    message.GetComponentInChildren<UnityEngine.UI.Text>().text = "Respawning...";
                }
            }
            
            yield return new WaitForSeconds(3f);

            if (message != null)
            {
                if (photonView.isMine)
                {
                    message.GetComponentInChildren<UnityEngine.UI.Text>().text = "";
                }
            }

            health = maxHealth;

            // pick a random spawn location
            Transform spawnPoint = spawnPoints[spawnIndex].transform;

            // teleport player to spawn point
            transform.position = spawnPoint.position;
            GetComponent<PlayerControllerV2>().newPosition = spawnPoint.position;
            GetComponent<PhotonView>().RPC("TeleportPlayer", PhotonTargets.All, spawnPoint.position);

            dead = false;
        }

        #endregion

    }
}

