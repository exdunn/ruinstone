using UnityEngine;
using System;
using System.Collections.Generic;

namespace SpellSystem {
    public class P_Fireball : Projectile {

        void Start() {

            explosionPrefab = "Effects/FireExplosion";
        }

        public override void Dissipate() {
            //Put death stuff here
            Die();
        }

        protected override void OnCollide(GameObject target) {

            SpellUtility.Damage(target, caster, _stats.damage);

            // if projectile has an explosion prefab then instantiate it
            if (explosionPrefab != null)
            {
                CreateExplosion(target.transform.position, 2f, 3f);
            }

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

