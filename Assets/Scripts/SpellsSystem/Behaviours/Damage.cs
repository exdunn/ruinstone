using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Damage : Payload {
    void Start() {
        _abilityType = Types.Ability.DAMAGE;
    }

    public override void DoEffect(GameObject caster, GameObject target, Vector3 point) {
        this.caster = caster;
        this.target = target;
        this.point = point;
        if(_duration > 0) {
            StartCoroutine(DuraEffect());
        }
        else {
            Effect();
        }
    }

    protected override void Effect() {
        if(_area == 0) {
            //Debug.Log("Did damage.");
            if(target == null) {
                //Debug.Log("Target is null when it should not be. Did you forget to load up target from a previous module?");
                //Do nothing, since there is no target
            }
            else {
                DoEffect(target);
            }
        }
        else if(_area > 0) {
            List<GameObject> targets = Utils.GetAll(_targetType, point, _area);
            for(int i = 0; i < targets.Count; ++i) {
                //Do stuff to the target
                Debug.Log("Did damage to" + i + ".");
                DoEffect(targets[i]);
            }
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
        _internal = 0f;
    }

    private void DoEffect(GameObject t) {
        WizardWars.PlayerManager player = t.GetComponent<WizardWars.PlayerManager>();
        player.UpdateHealth(-_power);
    }
}
