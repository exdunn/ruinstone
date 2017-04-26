using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class Direct : Delivery {
    //Does nothing, or sets target to target
    public override void DoEffect(GameObject caster, GameObject target, Transform point) {
        Finish();
    }

    protected override IEnumerator DuraEffect() {
        yield return null;
    }

    protected override void Effect() {
        //Do Nothing
    }

    protected override void Finish() {
        isDone = true;
    }
}
