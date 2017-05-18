using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardWars;

namespace SpellSystem {
    public class S008_Innervate : Spell {
        public Status _status;

        protected override IEnumerator StartCast(GameObject caster, GameObject target, Vector3 point) {
            if(!_status) {
                Debug.Log("_status is empty!");
            }

            SpellUtility.Status("Spells/U_Innervate", caster);
            StartCoroutine(Cooldown(caster));
            yield return null;
        }
    }
}

