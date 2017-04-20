using UnityEngine;
using System.Collections;

public class Teleport : Payload {
    public Transform blink { get; set; }

    void Start() {
        _abilityType = Types.Ability.DISPLACE;
        _areaType = Types.Area.POINT;
        _targetType = Types.Target.SELF;
    }

    public override void DuraEffect() {
        Debug.Log("DuraEffect in Teleport does nothing.");
    }

    public override void Effect() {
        //First target in targets should be self
        //Get move component
        //Call teleport or something
    }

    protected override IEnumerator Duration() {
        yield return null;
    }

    protected override void Finish() {
        isDone = true;
    }
}
