using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellSystem {
    public class S005_EntropicDecay : DelayedSpell {

        public string status;
        public string indicator;

        // duration of the status applied
        public float duration;

        public override void Cast(GameObject caster, GameObject target, Vector3 point)
        {
            bool p = Precast(caster, target, point);
            if (!p) {
                return;
            }

            point = SpellUtility.LevelPoint(point, 0.5f);

            GameObject i = SpellUtility.SpawnIndicator("Spells/I/" + indicator, this.transform, point, Quaternion.identity, _stats.areaRadius);

            StartCoroutine(SpellUtility.AreaStatusOverTime(Types.Target.ENEMY, point, _stats.areaRadius, "Spells/U/" + status, _stats.duration, duration, i));
            StartCoroutine(Cooldown(caster));
        }

        protected override IEnumerator DelayedCast(GameObject caster, GameObject target, Vector3 point) {
            //Create an area at target location for 3 seconds.
            //Any enemy in that area is debuffed.
            //Create a status that increase damage taken and reduces movement speed
            bool p = Precast(caster, target, point);
            if(p) {
                StartCoroutine(SpellUtility.Delay(_stats.delay, this));
                while(!delay) {
                    yield return null;
                }
                
            }
            yield return null;
        }
    }
}

