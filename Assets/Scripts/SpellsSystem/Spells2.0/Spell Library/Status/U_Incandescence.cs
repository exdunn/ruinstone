using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

using WizardWars;
namespace SpellSystem
{
    public class U_Incandescence : Status
    {
        public float _damageModifier;
        public float _damageReceivedModifier;

        public override void Activate(GameObject target, int where)
        {
            if (isStarting)
            {
                return;
            }
            isStarting = true;
            _timer = 0f;
            this.target = target;
            this.where = where;
            PlayerManager player = target.GetComponent<PlayerManager>();
            if (!player)
            {
                return;
            }

            // modify player stats
            player.GetComponent<PhotonView>().RPC("UpdateDamageModifier", PhotonTargets.All, player.damageModifier + _damageModifier);
            player.GetComponent<PhotonView>().RPC("UpdateDamageReceivedModifier", PhotonTargets.All, player.damageReceivedModifier + _damageReceivedModifier);

            StartCoroutine(Run());
        }

        public override void Deactivate(GameObject target)
        {
            //Undo the movement speed change
            PlayerManager player = target.GetComponent<PlayerManager>();
            if (!player)
            {
                return;
            }

            // return player stats to their original value
            player.GetComponent<PhotonView>().RPC("UpdateDamageModifier", PhotonTargets.All, player.damageModifier - _damageModifier);
            player.GetComponent<PhotonView>().RPC("UpdateDamageReceivedModifier", PhotonTargets.All, player.damageReceivedModifier - _damageReceivedModifier);
        }

        protected override IEnumerator Run()
        {
            while (_timer < _duration)
            {
                yield return new WaitForSeconds(SpellUtility.TICK);
                _timer += SpellUtility.TICK;
            }
            isEnd = true;
        }
    }
}

