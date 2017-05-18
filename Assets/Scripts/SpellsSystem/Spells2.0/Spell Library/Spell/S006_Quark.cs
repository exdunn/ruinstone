using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

namespace SpellSystem {
    public class S006_Quark : Spell {
        public string spawn; //Eventually place this into SpellStats

        public override void Cast(GameObject caster, GameObject target, Vector3 point) {
            bool p = Precast(caster, target, point);
            if(!p) {
                return;
            }
            Vector3 spawnPos = new Vector3(caster.transform.position.x, caster.transform.position.y + 1, caster.transform.position.z);

            GameObject proj = SpellUtility.SpawnProjectile("Spells/" + spawn, this.transform, spawnPos, Quaternion.identity, _stats.radius);
            proj.GetComponent<Projectile>()._stats = _stats;
            proj.GetComponent<Projectile>().Move(caster, point, 0f);
            StartCoroutine(Cooldown(caster));
        }
    }
}

