﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

using WizardWars;
namespace SpellSystem {
    public class U_Slow : Status {
        public float _moveSpeedModifier;

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
            player.GetComponent<PhotonView>().RPC("UpdateMoveSpeedModifier", PhotonTargets.All, player.moveSpeedModifier + _moveSpeedModifier);
            //player.cooldownReduction += _cooldownReduction;

            StartCoroutine(Run());
        }

        public override void Deactivate(GameObject target) {
            //Undo the movement speed change
            PlayerManager player = target.GetComponent<PlayerManager>();
            if(!player) {
                return;
            }
            player.GetComponent<PhotonView>().RPC("UpdateMoveSpeedModifier", PhotonTargets.All, player.moveSpeedModifier - _moveSpeedModifier);
            // player.cooldownReduction -= _cooldownReduction;
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

