using UnityEngine;
using System;
using System.Collections.Generic;

namespace SpellSystem {
    public class P_PlasmaOrb : Projectile {
        public override void Dissipate() {
            //Put death stuff here
            Die();
        }

        protected override void OnCollide(GameObject target) {
            SpellUtility.Damage(target, caster, _stats.damage);
            SpellUtility.Status("Spells/U_PlasmaOrb", target);
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

