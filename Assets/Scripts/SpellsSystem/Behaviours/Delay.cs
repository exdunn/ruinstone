using UnityEngine;
using System.Collections;
using System;

public class Delay : Behaviour {
    public float _duration = 0f;

    private float _internal = 0f;

    void Start() {
        _targetType = Types.Target.SELF;
    }

    public override void DoEffect(GameObject caster, GameObject target, Transform point) {
        StartCoroutine(DuraEffect());
    }

    protected override void Effect() {
        Debug.Log("Effect in Delay does nothing.");
    }

    protected override IEnumerator DuraEffect() {
        while(_internal < _duration) {
            yield return new WaitForSeconds(TICK);
            _internal += TICK;
        }
        Finish();
    }

    protected override void Finish() {
        isDone = true;
    }
}
