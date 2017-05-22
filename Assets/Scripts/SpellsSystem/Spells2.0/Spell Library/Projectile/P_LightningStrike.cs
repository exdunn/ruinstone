using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellSystem {
    public class P_LightningStrike : Projectile {
        public GameObject indicator;

        void Start() {

            explosionPrefab = "Effects/LightningExplosion";
        } 

        public override void Dissipate() {
            //Explode
            if(indicator) {
                PhotonNetwork.Destroy(indicator);
            }
            
            Die();
        }


        protected override void OnCollide(GameObject target) {
            Debug.Log("On Collision");
            //this.target = SpellUtility.LevelPoint(this.target);
            SpellUtility.AreaDamage(Types.Target.ENEMY, caster, this.target, _stats.areaRadius, _stats.damage);

            // if projectile has an explosion prefab then instantiate it
            if (explosionPrefab != null)
            {
                CreateExplosion(transform.position, 5f, 1f);
            }

            Dissipate();
        }

        protected override void OnLocation() {
            Debug.Log("On Location");

            // if projectile has an explosion prefab then instantiate it
            if (explosionPrefab != null)
            {
                CreateExplosion(transform.position, 5f, 1f);
            }

            this.target = SpellUtility.LevelPoint(this.target);
            SpellUtility.AreaDamage(Types.Target.ENEMY, caster, target, _stats.areaRadius, _stats.damage);
            Dissipate();
        }

        protected override void OnOutOfRange() {
            Dissipate();
        }
    }
}

