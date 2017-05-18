using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using WizardWars;

namespace SpellSystem {
    public abstract class DelayedSpell : Spell {
        public override void Cast(GameObject caster, GameObject target, Vector3 point) {
            bool p = Precast(caster, target, point);
            if(!p) {
                return;
            }

            StartCoroutine(DelayedCast(caster, target, point));
        }

        protected abstract IEnumerator DelayedCast(GameObject caster, GameObject target, Vector3 point);
    }
}




