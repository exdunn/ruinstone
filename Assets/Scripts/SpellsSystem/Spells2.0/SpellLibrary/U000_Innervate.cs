using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

using WizardWars;
namespace SpellSystem {
    public class U000_Innervate : Status {

        public override void Activate(GameObject target) {
            if(isStarting) {
                return;
            }
            isStarting = true;
            _timer = 0f;
            //Increase movement speed
            

            StartCoroutine(Run());
            
        }

        public override void Deactivate(GameObject target) {
            //Undo the movement speed change
        }

        protected override IEnumerator Run() {
            while(_timer < _duration) {
                yield return new WaitForSeconds(SpellUtility.TICK);
                _timer += SpellUtility.TICK;
            }
            isDone = true;
        }
    }
}

