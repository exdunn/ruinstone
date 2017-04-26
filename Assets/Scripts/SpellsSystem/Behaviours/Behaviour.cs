using UnityEngine;
using System.Collections;
//TODO;
//Create a dummy Delivery called Direct
//Deliveries will have the range, Area Type, etc.
//Deliveries will not have Ability Type, but will have target type
//Deliveries will not have recharge (in fact no module will have recharge ) - this is unique to only spells
//Payloads will not have range, area type, nor recharge
//Payloads will have ability type, target type, power,duration, area

    //Idea is every spell has a delivery mechanism that determines how far the targets can be selected, or how far it can travel and what kind of travelling does it do
    //Payloads on the other hand are what it does once it reaches a destination

//Test the other components

/* Abstract Base Behaviour class */
public abstract class Behaviour : MonoBehaviour {
    protected const float TICK = 1f;

    public Types.Target _targetType = Types.Target.ENEMY;

    public bool isDone { get; set; }
    public GameObject caster { get; set; }
    public GameObject target { get; set; }
    public Transform point { get; set; }

    void Start() {
        isDone = false;
    }

    public abstract void DoEffect(GameObject caster, GameObject target, Transform point);

    protected abstract void Effect();
    protected abstract IEnumerator DuraEffect();
    protected abstract void Finish();
}
