using UnityEngine;
using System.Collections;

public class Heal : Payload {
    void Start() {
        _abilityType = Types.Ability.DAMAGE;
    }

    void OnEnable() {
        if(_duration > 0) {
            DuraEffect();
        }
        else {
            Effect();
        }
    }

    void OnDisable() {
        Finish();
    }

    public override void DuraEffect() {
        StartCoroutine(Duration());
    }

    public override void Effect() {
        //For each target, do power damage to it
        while(targets.Count > 0) {
            GameObject t = targets.Dequeue();
            //Get Health component of t
            //If cannot, skip
            //Call set health or something
        }
    }

    protected override IEnumerator Duration() {
        while(_internal < _duration) {
            Effect();
            yield return new WaitForSeconds(TICK);
            _internal += TICK;
        }
    }

    protected override void Finish() {
        isDone = true;
    }
}
