using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

using WizardWars;
namespace SpellSystem {

    public class U_VoidWalk : Status {

        public float _moveSpeedBonus;
        public float _damageReceivedModifier;
        public float secondaryDur;
        
        public float damage { get; set; }

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

            // modify player stats
            player.GetComponent<PhotonView>().RPC("UpdateMoveSpeedModifier", PhotonTargets.All, player.moveSpeedModifier + _moveSpeedBonus);
            player.GetComponent<PhotonView>().RPC("UpdateDamageReceivedModifier", PhotonTargets.All, player.damageReceivedModifier + _damageReceivedModifier);

            StartCoroutine(Run());
        }

        public override void Deactivate(GameObject target) {
            //Undo the movement speed change
            PlayerManager player = target.GetComponent<PlayerManager>();
            if(!player) {
                return;
            }

            // return player stats to their original value
            player.GetComponent<PhotonView>().RPC("UpdateMoveSpeedModifier", PhotonTargets.All, player.moveSpeedModifier - _moveSpeedBonus);
            player.GetComponent<PhotonView>().RPC("UpdateDamageReceivedModifier", PhotonTargets.All, player.damageReceivedModifier - _damageReceivedModifier);
        }

        protected override IEnumerator Run() {
            while(_timer < _duration) {
                yield return new WaitForSeconds(SpellUtility.TICK);
                _timer += SpellUtility.TICK;
            }

            isEnd = true;
        }

        // when Voidwalking player collides with another player
        // deal damage to and slow the other player
        void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Player") {
                return;
            }

            if (other.gameObject.GetComponent<PlayerManager>().playerId != target.GetComponent<PlayerManager>().playerId) {

                SpellUtility.Damage(other.gameObject, target, damage);
                SpellUtility.Status("Spells/U/U_PlasmaOrb", other.gameObject, secondaryDur);
                //Debug.Log("collision");
            }
        }
    }
}

