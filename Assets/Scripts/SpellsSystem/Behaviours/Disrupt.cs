using UnityEngine;
using System.Collections;
using System;

public class Disrupt : Payload {
    public Names.Status _status;

    void Start() {
        _abilityType = Types.Ability.DISRUPT;
    }

    public override void DoEffect(GameObject caster, Transform target) {
        this.caster = caster;
        this.target = target;
        if(_duration > 0) {
            StartCoroutine(DuraEffect());
        }
        else {
            Effect();
        }
    }

    protected override void Effect() {
        while(targets.Count > 0) {
            GameObject t = targets.Dequeue();
            //Get Status component of t
            //If cannot, skip
            //Call set status or something
        }
        Finish();
    }

    protected override IEnumerator DuraEffect() {
        while(_internal < _duration) {
            Effect();
            yield return new WaitForSeconds(TICK);
            _internal += TICK;
        }
        Finish();
    }

    protected override void Finish() {
        isDone = true;
        //Note: Statuses, when finished, are taken care of by themselves (they have an Undo method) in the Character
    }
}
