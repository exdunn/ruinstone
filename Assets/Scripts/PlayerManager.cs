using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardWars
{
    public class PlayerManager : Photon.PunBehaviour {

        #region Public Variables

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        [Tooltip("The current Health of our player")]
        public int Health = 100;

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
            if (Health <= 0)
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


            // rule out any trigger not tagged "Beam". We are only interested in Beamers
            if (!other.CompareTag("Firebolt"))
            {
                Health -= 15;
            }   
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion

    }
}

