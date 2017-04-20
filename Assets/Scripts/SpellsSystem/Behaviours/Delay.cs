using UnityEngine;
using System.Collections;

public class Delay : Behaviour {
    public float _duration = 0f;

    private float _internal = 0f;

    void Start() {
        _abilityType = Types.Ability.MISC;
        _areaType = Types.Area.POINT;
        _targetType = Types.Target.SELF;
    }

    void OnEnable() {
        DuraEffect();
    }

    void OnDisable() {
        Finish();
    }

    public override void DuraEffect() {
        StartCoroutine(Duration());
        Finish();
    }

    public override void Effect() {
        Debug.Log("Effect in Delay does nothing.");
    }

    protected override IEnumerator Duration() {
        while(_internal < _duration) {
            yield return new WaitForSeconds(TICK);
            _internal += TICK;
        }
    }

    protected override void Finish() {
        isDone = true;
    }
}
