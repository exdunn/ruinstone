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

        public int playerId
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
            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(this.gameObject);
        }

        void Start()
        {
            deaths = 0;
            kills = 0;

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
            if (health == 0)
            {


                // player death anim
                GetComponent<PhotonView>().RPC("ReceivedDieAnim", PhotonTargets.All, true);

                // tell game manager player is dead
                gameManager.GetComponent<GameManager>().PlayerDie(playerId);
            }
        } 

        #endregion

        #region Public Methods

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
        }

        [PunRPC]
        public void ReceivedDieAnim(bool die)
        {
            GetComponent<Animator>().SetBool("dead", die);
        }

        #endregion

        #region Private Methods



        #endregion

    }
}

