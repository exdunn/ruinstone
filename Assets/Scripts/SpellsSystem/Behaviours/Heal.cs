using UnityEngine;
using System.Collections;

public class Heal : Payload {
    void Start() {
        _abilityType = Types.Ability.DAMAGE;
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
        //For each target, do power damage to it
        while(targets.Count > 0) {
            GameObject t = targets.Dequeue();
            //Get Health component of t
            //If cannot, skip
            //Call set health or something
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
    }
}
