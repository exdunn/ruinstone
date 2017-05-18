using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellSystem {
    public class S_Meteor : Spell {
        public string meteor;
        public string indicator;
        public float height;

        protected override IEnumerator StartCast(GameObject caster, GameObject target, Vector3 point) {
            Vector3 spawnPos = new Vector3(point.x, point.y + height, point.z);
            GameObject m = SpellUtility.SpawnProjectile("Spells/" + meteor, this.transform, spawnPos, Quaternion.identity, _stats.radius);
            m.GetComponent<Projectile>()._stats = _stats;
            GameObject i = SpellUtility.SpawnIndicator("Spells/" + indicator, this.transform, point, Quaternion.identity, _stats.radius);
            m.GetComponent<P_Meteor>().indicator = i;

            m.GetComponent<Projectile>().Move(caster, point, height);
            StartCoroutine(Cooldown(caster));
            yield return null;
        }
    }
}

