using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardWars;

namespace SpellSystem {
    public class S003_Incandescence : Spell {
        public Status _status;

        public override void Cast(GameObject caster, GameObject target, Vector3 point) {
            //Check caster
            if(!caster) {
                Debug.Log("Caster is empty!");
            }

            if(!_status)
            {
                Debug.Log("_status is empty!");
            }

            SpellUtility.Status("Spells/U_Incandescence", caster);
            isCastable = false;
            StartCoroutine(Cooldown(caster));
        }

    }
}

