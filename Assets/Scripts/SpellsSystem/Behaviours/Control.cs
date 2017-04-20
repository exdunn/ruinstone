using UnityEngine;
using System.Collections;

public class Control : Payload {
    public Names.CrowdControl _cc;

    void Start() {
        _abilityType = Types.Ability.DISRUPT;
    }

    public override void DuraEffect() {
        StartCoroutine(Duration());
    }

    public override void Effect() {
        while(targets.Count > 0) {
            GameObject t = targets.Dequeue();
            //Get Status component of t
            //If cannot, skip
            //Call set status or something
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
        //Note: Statuses, when finished, are taken care of by themselves (they have an Undo method) in the Character
    }
}
