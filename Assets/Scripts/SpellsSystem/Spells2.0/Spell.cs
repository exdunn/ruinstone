﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using WizardWars;

namespace SpellSystem
{
    public abstract class Spell : MonoBehaviour
    {

        public SpellStats _stats;
        private float _cooldownTimer = 0f;
        private float _spellTimer = 0f;

        public bool isCastable { get; set; }

        void Start() {
            _stats = GetComponent<SpellStats>();
            isCastable = true;
        }

        //Primary Activation Method
        //Question: Should this be a coroutine?
        public abstract void Cast(GameObject caster, GameObject target, Vector3 point);

        protected IEnumerator Cooldown(GameObject caster)
        {
            //float cd = _stats.GetCooldown() - (_stats.GetCooldown() * caster's cdr)
            //cd = cd if cd >= 0 else 0
            while(_cooldownTimer <= _stats.cooldown)
            {
                yield return new WaitForSeconds(SpellUtility.TICK);
                _cooldownTimer += SpellUtility.TICK;
            }
            isCastable = true;
        }
    }
}

