using UnityEngine;
using System;
using System.Collections.Generic;

namespace SpellSystem {
    public class S007_PlasmaOrb : Spell {
        public string spawn; //Eventually place this into SpellStats
        public override void Cast(GameObject caster, GameObject target, Vector3 point) {
            //Target is not used for Fireball (It is a directional skill shot)
            if(caster == null) {
                Debug.Log(this + ": \n" + "Caster is null...");
            }
            if(!isCastable) {
                Debug.Log(this + " is not castable yet.");
                return;
            }
            isCastable = false;

            Vector3 spawnPos = new Vector3(caster.transform.position.x, caster.transform.position.y + 1, caster.transform.position.z);

            GameObject proj = SpellUtility.SpawnProjectile("Spells/" + spawn, this.transform, spawnPos, Quaternion.identity, _stats.GetRadius());
            proj.GetComponent<Projectile>()._stats = _stats;
            proj.GetComponent<Projectile>().Move(caster, point);
            StartCoroutine(Cooldown(caster));
        }
    }
}

