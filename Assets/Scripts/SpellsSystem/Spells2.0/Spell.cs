using UnityEngine;
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

        protected IEnumerator Cooldown()
        {
            while(_cooldownTimer <= _stats.GetCooldown())
            {
                yield return new WaitForSeconds(SpellUtility.TICK);
                _cooldownTimer += SpellUtility.TICK;
            }
            isCastable = true;
        }
    }
}

