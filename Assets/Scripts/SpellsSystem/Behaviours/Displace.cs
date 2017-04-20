using UnityEngine;
using System.Collections;

public class Displace : Payload {
    void Start() {
        _abilityType = Types.Ability.DISPLACE;
    }

    public override void DuraEffect() {
        StartCoroutine(Duration());
    }

    public override void Effect() {
        while(targets.Count > 0) {
            GameObject t = targets.Dequeue();
            //Get Move component of t
            //If cannot, skip
            //Call move or something
        }
    }

    protected override IEnumerator Duration() {
        while(_internal <_duration) {
            Effect();
            yield return new WaitForSeconds(TICK);
            _internal += TICK;
        }
    }

    protected override void Finish() {
        isDone = true;
    }
}
