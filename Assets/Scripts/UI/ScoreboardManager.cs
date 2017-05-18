using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardWars {

    public class ScoreboardManager : Photon.MonoBehaviour {

        #region variables 

        public GameManager gameManager;
        public GameObject labelPrefab;
        public Dictionary<int, ScoreboardLabelUI> scorebaordLabels;

        #endregion

        #region monobehaviour callbacks
        // Use this for initialization
        void Start () {

            scorebaordLabels = new Dictionary<int, ScoreboardLabelUI>();

            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                GameObject newLabel = Instantiate(labelPrefab, gameObject.transform);
                scorebaordLabels[(int)player.CustomProperties["ID"]] = newLabel.GetComponent<ScoreboardLabelUI>();
            }
	    }
        
	    // Update is called once per frame
	    void Update () {
		
	    }

        #endregion

        #region public methods

        public void UpdateScoreLabelName(int key, string name)
        {
            scorebaordLabels[key].gameObject.GetComponent<PhotonView>().
                RPC("BroadcastNameTextChange",
                PhotonTargets.All,
                name);
        }

        public void UpdateScoreLabelKills(int key, int kills)
        {
            scorebaordLabels[key].gameObject.GetComponent<PhotonView>().
                RPC("BroadcastKillsTextChange", 
                PhotonTargets.All,
                "Kills: " + kills);
        }

        public void UpdateScoreLabelDeaths(int key, int deaths)
        {
            scorebaordLabels[key].GetComponent<PhotonView>().
                RPC("BroadcastDeathsTextChange", 
                PhotonTargets.All,
                "Deaths: " + deaths);
        }

        public void UpdateScoreLabelDamage(int key, float damage)
        {
            scorebaordLabels[key].GetComponent<PhotonView>().
                RPC("BroadcastDamageTextChange", 
                PhotonTargets.All, 
                "Damage: " + damage);
        }

        #endregion
    }
}
