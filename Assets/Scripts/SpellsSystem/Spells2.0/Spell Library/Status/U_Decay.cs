using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardWars;
namespace SpellSystem {
    public class U_Decay : Status {
        public float _damageReceivedModifier;
        public float _slowModifier;
        public override void Activate(GameObject target, int where) {
            if(isStarting) {
                return;
            }
            isStarting = true;
            _timer = 0f;
            this.target = target;
            this.where = where;
            PlayerManager player = target.GetComponent<PlayerManager>();
            if(!player) {
                return;
            }

            //player.GetComponent<PhotonView>().RPC("UpdateDamageModifier", PhotonTargets.All, player.damageModifier + _damageModifier);
            player.GetComponent<PhotonView>().RPC("UpdateDamageReceivedModifier", PhotonTargets.All, player.damageReceivedModifier + _damageReceivedModifier);

            StartCoroutine(Run());
        }

        public override void Deactivate(GameObject target) {
            PlayerManager player = target.GetComponent<PlayerManager>();
            if(!player) {
                return;
            }
            //player.GetComponent<PhotonView>().RPC("UpdateDamageModifier", PhotonTargets.All, player.damageModifier + _damageModifier);
            player.GetComponent<PhotonView>().RPC("UpdateDamageReceivedModifier", PhotonTargets.All, player.damageReceivedModifier + _damageReceivedModifier);
        }

        protected override IEnumerator Run() {
            while(_timer < _duration) {
                yield return new WaitForSeconds(SpellUtility.TICK);
                _timer += SpellUtility.TICK;
            }
            isEnd = true;
        }

    }
}

