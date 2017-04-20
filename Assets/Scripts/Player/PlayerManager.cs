using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WizardWars
{
    public class PlayerManager : Photon.PunBehaviour {

        #region Public Variables

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        [Tooltip("UI element that displays player's current health")]
        public GameObject healthGlobe;

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

        #endregion

        #region MonoBehaviour Callbacks


        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        void Awake()
        {
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.isMine)
            {
                PlayerManager.LocalPlayerInstance = this.gameObject;
            }
            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        void Start()
        {
            deaths = 0;
            kills = 0;

            CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();

            if (_cameraWork != null)
            {
                if (photonView.isMine)
                {
                    _cameraWork.OnStartFollowing();
                }
            }
            else {
                Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
            }
        }

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity on every frame.
        /// </summary>
        void Update()
        {
            if (health <= 0)
            {
                Debug.Log("Player has died");
            }
        }

        /// <summary>
        /// MonoBehaviour method called when the Collider 'other' enters the trigger.
        /// Affect Health of the Player if the collider is a beam
        /// </summary>
        void OnTriggerEnter(Collider other)
        {
            if (!photonView.isMine)
            {
                return;
            }


            if (!other.CompareTag("Firebolt"))
            {
                health -= 15;
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

            // update heatlh bar
            healthGlobe.GetComponent<HealthGlobe>().SetFill(health / maxHealth);
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

        #region Private Methods



        #endregion

    }
}

