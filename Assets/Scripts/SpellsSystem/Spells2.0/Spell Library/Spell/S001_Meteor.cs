using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellSystem {
    public class S001_Meteor : Spell {
        public string meteor;
        public string indicator;
        public float height;

        public override void Cast (GameObject caster, GameObject target, Vector3 point) {
            bool p = Precast(caster, target, point);
            if(!p) {
                return;
            }
            Debug.Log("point : " + point);
            point = SpellUtility.LevelPoint(point);
            Debug.Log("new point: " + point);
            Vector3 spawnPos = new Vector3(point.x, point.y + height, point.z);
            GameObject m = SpellUtility.SpawnProjectile("Spells/P/" + meteor, this.transform, spawnPos, Quaternion.identity, _stats.radius);
            m.GetComponent<Projectile>()._stats = _stats;
            m.GetComponent<Projectile>()._targetType = Types.Target.GROUND;
            GameObject i = SpellUtility.SpawnIndicator("Spells/I/" + indicator, this.transform, point, Quaternion.identity, _stats.radius);
            m.GetComponent<P_Meteor>().indicator = i;

            m.GetComponent<Projectile>().Move(caster, point, height);
            StartCoroutine(Cooldown(caster));
        }
    }
}

