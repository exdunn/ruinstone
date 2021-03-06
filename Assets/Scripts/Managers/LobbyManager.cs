﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace WizardWars
{
    public class LobbyManager : Photon.PunBehaviour
    {
        #region Public Variables

        public GameObject labelPrefab;
        public GameObject playersPanel;
        public GameObject spellsPanel;

        SpellSlotUI[] spellSlots;
        SpellStats[] library;

        #endregion

        #region Public Methods

        /// <summary>
        /// If the user is the master client then load game scene
        /// </summary>
        public void StartClick()
        {
            AssignIds();

            if (!PhotonNetwork.isMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            }
            // if player is the master then close the room and load arena
            else
            {
                PhotonNetwork.room.IsOpen = false;
                PhotonNetwork.LoadLevel("dunn test scene");
            }
        }

        public void SpellsClick()
        {
            int index = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<PresetSelectButton>().index;
            int[] spellIds = new int[GlobalVariable.DECKSIZE];

            switch(index)
            {
                case 1:
                    spellIds = PlayerPrefsX.GetIntArray("Spells1");
                    break;
                case 2:
                    spellIds = PlayerPrefsX.GetIntArray("Spells2");
                    break;
                case 3:
                    spellIds = PlayerPrefsX.GetIntArray("Spells3");
                    break;
                case 4:
                    spellIds = PlayerPrefsX.GetIntArray("Spells4");
                    break;
                default:
                    break;
            }

            SetSpellIcons(spellIds);
            PlayerPrefsX.SetIntArray("CurSpells", spellIds);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Set the spell icons to match the selected preset
        /// </summary>
        /// <param name="spellIds"></param>
        private void SetSpellIcons(int[] spellIds)
        {
            for (int i = 0; i < spellIds.Length; i++)
            {
                spellSlots[i].spellIcon.GetComponent<Image>().sprite = library[spellIds[i]].iconSprite;
            }
        }

        private void AssignIds()
        {
            List<int> duplicates = new List<int>();

            int i = 0;
            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable() { { "ID", i++ } };
                player.SetCustomProperties(hash);
            }          
        }

        #endregion

        #region monobehaviour callbacks

        // Use this for initialization
        void Start()
        {
            library = GameObject.FindGameObjectWithTag("Library").GetComponents<SpellStats>();
            spellSlots = GetComponentsInChildren<SpellSlotUI>();
            InstantiatePlayerLabels();
            SetSpellIcons(PlayerPrefsX.GetIntArray("Spells1"));

            // set current spells to Spells 1
            PlayerPrefsX.SetIntArray("CurSpells", PlayerPrefsX.GetIntArray("Spells1"));
        }

        /// <summary>
        /// Get list of players in the room and instantiate a player label for each one in the players panel
        /// </summary>
        void InstantiatePlayerLabels()
        {
            // get list of players in the room
            PhotonPlayer[] players = PhotonNetwork.playerList;

            // instantiate player label for each room in roomList
            foreach (PhotonPlayer player in players)
            {
                //Debug.Log("Player: " + player.NickName + "id: " + player.ID);
                 // instantiate label prefab
                GameObject newPlayerLabel = Instantiate(labelPrefab, playersPanel.transform.position, playersPanel.transform.rotation, playersPanel.transform);
                newPlayerLabel.GetComponent<PlayerLabel>().nameText.text = player.NickName;
            }
        }

        #endregion

        #region Photon.PunBehaviour CallBacks

        public override void OnPhotonPlayerDisconnected(PhotonPlayer player)
        {
            PlayerLabel[] playerLabels = GetComponentsInChildren<PlayerLabel>();

            foreach (PlayerLabel label in playerLabels)
            {
                if (player.ID == label.playerNum)
                {
                    Destroy(label.gameObject);
                }
            }
        }

        public override void OnMasterClientSwitched(PhotonPlayer player)
        {
            Debug.Log("OnMasterClientSwitched() called");
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene(0);
        }

        public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
        {
            GameObject newPlayerLabel = Instantiate(labelPrefab, playersPanel.transform.position, playersPanel.transform.rotation, playersPanel.transform);
            newPlayerLabel.GetComponent<PlayerLabel>().nameText.text = newPlayer.NickName;
            newPlayerLabel.GetComponent<PlayerLabel>().playerNum = newPlayer.ID;
        }

        #endregion

    }
}

