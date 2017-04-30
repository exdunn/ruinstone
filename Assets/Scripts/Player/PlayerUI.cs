using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WizardWars
{
    public class PlayerUI : MonoBehaviour
    {
        public GameObject[] spellBar;

        public PlayerManager player
        {
            get; set;
        }
        public HealthGlobeUI healthGlobe;


        // Use this for initialization
        void Awake()
        {
            healthGlobe = GetComponentInChildren<HealthGlobeUI>();
            GetComponent<Transform>().SetParent(GameObject.Find("Canvas").GetComponent<Transform>());
        }

        // Update is called once per frame
        void Update()
        {
            // set fill of health globe to match player's current health
            if (healthGlobe != null)
            {
                healthGlobe.SetFill(1 - player.health/player.maxHealth);
            }
        }

        public void SetTarget(PlayerManager player)
        {
            if (player == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
                return;
            }

           this.player = player;
            Debug.Log("player: " + this.player);
        }
    }

}
