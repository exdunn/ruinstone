using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

namespace SpellSystem {
    public class S007_PlasmaOrb : Spell {
        public string spawn; //Eventually place this into SpellStats

        protected override IEnumerator StartCast(GameObject caster, GameObject target, Vector3 point) {
            Vector3 spawnPos = new Vector3(caster.transform.position.x, caster.transform.position.y + 1, caster.transform.position.z);

            GameObject proj = SpellUtility.SpawnProjectile("Spells/" + spawn, this.transform, spawnPos, Quaternion.identity, _stats.radius);
            proj.GetComponent<Projectile>()._stats = _stats;
            proj.GetComponent<Projectile>().Move(caster, point, 0f);
            StartCoroutine(Cooldown(caster));
            yield return null;
        }
    }
}

