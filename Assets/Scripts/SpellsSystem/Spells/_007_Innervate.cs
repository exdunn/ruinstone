using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using WizardWars;

public class _007_Innervate : Spell {

    private GameObject _spawn;
    public int _status;

    //private int i = 0;
    void Awake() {
        if(_delivery == null) {
            Debug.Log("There must be a delivery method!");
        }
    }

    public override void Activate(GameObject caster, GameObject target, Vector3 point) {

        //isActive = true;
        //PlayerManager player = caster.GetComponent<PlayerManager>();
        //if(player == null) {
        //    //Error
        //    Debug.Log("The caster isn't a player.");
        //    return;
        //}
        ////player.AddStatus(_status);
        //isActive = false;
        //Transform temp = Utils.CreateNewTransform(point);
        //StartCoroutine(CoActivate(caster, target, point));
        //Destroy(/*temp*/);
        GoOnCooldown();
    }

    public override void Finish() {
        isDone = true;
    }

    protected override IEnumerator CoActivate(GameObject caster, GameObject target, Vector3 point) {
        //Do Damage
        for(int i = 0; i < _behaviours.Length; ++i) {
            _behaviours[i].DoEffect(caster, target, point);
            while(!_behaviours[i].isDone) {
                yield return null;
            }
        }
        yield return null;
        isActive = false;
    }
}
