﻿using System.Collections;
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

        public SpellUI[] spells;
        SpellStats[] library;
        public HealthGlobeUI healthGlobe;

        private void Awake()
        {
            // set spell stats library
            library = GameObject.FindGameObjectWithTag("Library").GetComponents<SpellStats>();

            // get ids of spells currently in use
            int[] spellIds = PlayerPrefsX.GetIntArray("CurSpells");

            // set spells for each spell globe ui elemet
            for (int i = 0; i < spells.Length; i++)
            {
                spells[i].spell = library[spellIds[i]];
                spells[i].SetIcon(library[spellIds[i]].iconSprite);
            }
        }

        // Use this for initialization
        void Start()
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

                healthGlobe.tooltip.GetComponent<Text>().text = player.health + "/" + player.maxHealth;
            }
        }
        
        // attach ui to target player
        public void SetTarget(PlayerManager player)
        {
            if (player == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
                return;
            }

            this.player = player;
        }

        public void OnCast(int index)
        {
            spells[index].ResetCooldownTimer();
        }
    }

}
