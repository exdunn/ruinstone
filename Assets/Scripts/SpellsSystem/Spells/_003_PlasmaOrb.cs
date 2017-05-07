using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class _003_PlasmaOrb : Spell {

    private GameObject _spawn;
    //private int i = 0;
    void Awake() {
        if(_delivery == null) {
            Debug.Log("There must be a delivery method!");
        }
    }

    public override void Activate(GameObject caster, GameObject target, Vector3 point) {
        isActive = true;
        //Transform temp = Utils.CreateNewTransform(point);
        StartCoroutine(CoActivate(caster, target, point));
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
        isActive = false;
    }
}
