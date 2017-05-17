using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WizardWars
{
    public class PlayerUI : MonoBehaviour
    {
       
        public PlayerManager player
        {
            get; set;
        }

        SpellUI[] spells;
        SpellStats[] library;
        public HealthGlobeUI healthGlobe;
        

        // Use this for initialization
        void Start()
        {
            healthGlobe = GetComponentInChildren<HealthGlobeUI>();
            GetComponent<Transform>().SetParent(GameObject.Find("Canvas").GetComponent<Transform>());
            library = GameObject.FindGameObjectWithTag("Library").GetComponents<SpellStats>();
            spells = GetComponentsInChildren<SpellUI>();

            int[] spellIds = PlayerPrefsX.GetIntArray("CurSpells");

            for (int i = 0; i < spells.Length; i++)
            {
                spells[i].spell = library[spellIds[i]];
                spells[i].SetIcon(library[spellIds[i]].iconSprite);
            }
        } 

        // Update is called once per frame
        void Update()
        {
            // set fill of health globe to match player's current health
            if (healthGlobe != null)
            {
                healthGlobe.SetFill(1 - player.health/player.maxHealth);

                healthGlobe.tooltip.GetComponent<Text>().text = player.health + "/" + player.maxHealth;
            }
        }

        /// <summary>
        /// attaches local player to playerUI
        /// </summary>
        /// <param name="player"></param>
        public void SetTarget(PlayerManager player)
        {
            if (player == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
                return;
            }

            this.player = player;
        }
    }

}
