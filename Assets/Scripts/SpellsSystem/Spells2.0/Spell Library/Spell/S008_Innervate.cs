using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardWars;

namespace SpellSystem {
    public class S008_Innervate : Spell {
        public Status _status;

        public override void Cast(GameObject caster, GameObject target, Vector3 point) {
            bool p = Precast(caster, target, point);
            if(!p) {
                return;
            }
            if(!_status) {
                Debug.Log("_status is empty!");
            }

            SpellUtility.Status("Spells/U/U_Innervate", caster);
            StartCoroutine(Cooldown(caster));
        }
    }
}

