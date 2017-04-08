using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WizardWars
{
    public class LobbyManager : Photon.PunBehaviour
    {
        #region Public Variables

        /// <summary>
        /// Player label prefab
        /// </summary>
        public GameObject labelPrefab;

        /// <summary>
        /// Panel which is used as the parent when instantiating lobby labels
        /// </summary>
        public GameObject playersPanel;

        #endregion

        #region Private Variables

        #endregion

        #region Public Methods

        /// <summary>
        /// If the user is the master client then load game scene
        /// </summary>
        public void StartClick()
        {
            if (!PhotonNetwork.isMasterClient)
            { 
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            }
            else
            {
                PhotonNetwork.LoadLevel("Test Scene");
            }
        }

        #endregion

        #region Private Methods

        // Use this for initialization
        void Start()
        {
            InstantiatePlayerLabels();
        }

        /// <summary>
        /// Get list of players in the room and instantiate a player label for each one in the players panel
        /// </summary>
        void InstantiatePlayerLabels()
        {
            // destroy all children of playersPanel
            foreach(Transform child in playersPanel.GetComponentInChildren<Transform>())
            {
                GameObject.Destroy(child.gameObject);
            }

            // get list of players in the room
            PhotonPlayer[] players = PhotonNetwork.playerList;
            Debug.Log("# of players: " + players.Length);

            // instantiate player label for each room in roomList
            int i = 0;
            foreach (PhotonPlayer player in players)
            {
                Transform parentTransform = playersPanel.transform;
                GameObject newPlayerLabel = Instantiate(labelPrefab, new Vector3(0,0,0), parentTransform.rotation, parentTransform);
                newPlayerLabel.GetComponent<PlayerLabel>().SetNameLabel(player.NickName);

                // set position of instantiated label to top of the panel
                newPlayerLabel.GetComponent<RectTransform>().anchoredPosition = new Vector2(1, 0.5f);
                // move the new label down to line up with other player labels
                float height = newPlayerLabel.GetComponent<RectTransform>().rect.height;
                newPlayerLabel.transform.position += Vector3.up * (-(height / 2) + -height * i);
                i++;
            }
        }

        #endregion

        #region Photon.PunBehaviour CallBacks

        public override void OnPhotonPlayerDisconnected(PhotonPlayer player)
        {
            Debug.Log("OnPhotonPlayerDisconnected() called");
        }

        public override void OnMasterClientSwitched(PhotonPlayer player)
        {
            Debug.Log("OnMasterClientSwitched() called");
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene(0);
        }

        public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
        {
            Debug.Log("OnPhotonPlayerConnected() called");
            InstantiatePlayerLabels();
        }

        #endregion

    }
}

