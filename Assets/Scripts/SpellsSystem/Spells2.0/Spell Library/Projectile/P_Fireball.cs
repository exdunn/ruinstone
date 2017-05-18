using UnityEngine;
using System;
using System.Collections.Generic;

namespace SpellSystem {
    public class P_Fireball : Projectile {
        public override void Dissipate() {
            //Put death stuff here
            Die();
        }

        protected override void OnCollide(GameObject target) {

            // the damage of the spell is the spell damage * player's damage modifier
            float damage = _stats.damage * caster.GetComponent<WizardWars.PlayerManager>().damageModifier;
            SpellUtility.Damage(target, damage);
            Dissipate();
        }

        protected override void OnLocation() {
            //Not used
            Dissipate();
        }

        protected override void OnOutOfRange() {
            //Not used
            Dissipate();
        }
    }
}

