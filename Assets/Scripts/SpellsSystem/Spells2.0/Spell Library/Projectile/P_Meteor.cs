using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellSystem {
    public class P_Meteor : Projectile {
        public GameObject indicator;

        void Start() {

            explosionPrefab = "Effects/FireExplosion";
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
            
            Dissipate();
        }

        protected override void OnLocation() {
            Debug.Log("On Location");
            this.target = SpellUtility.LevelPoint(this.target);
            SpellUtility.AreaDamage(Types.Target.ENEMY, caster, target, _stats.areaRadius, _stats.damage);
            Dissipate();
        }

        protected override void OnOutOfRange() {
            Dissipate();
        }
    }
}

