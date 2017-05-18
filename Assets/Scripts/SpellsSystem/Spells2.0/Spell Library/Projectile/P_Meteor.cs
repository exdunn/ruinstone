using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellSystem {
    public class P_Meteor : Projectile {
        public GameObject indicator;

        public override void Dissipate() {
            //Explode
            PhotonNetwork.Destroy(indicator);
            Die();
        }

        protected override void OnCollide(GameObject target) {
            //Do nothing
        }

        protected override void OnLocation() {
            SpellUtility.AreaDamage(Types.Target.ENEMY, caster, target, _stats.radius, _stats.damage);
            Dissipate();
        }

        protected override void OnOutOfRange() {
            Dissipate();
        }
    }
}

