using UnityEngine;
using System.Collections;
//TODO;
//Ignore Passthrough on Projectile
//Behaviours need a GameObject target parameter on DoEffect
//Remove public target properties from Payloads
//Move GetAll into the Effect of Payloads

//Create dummy methods in PlayerManager for Heal, Damage, Status, CC, Displace, Teleport
//Test the other components

/* Abstract Base Behaviour class */
public abstract class Behaviour : MonoBehaviour {
    protected const float TICK = .1f;

    public Types.Ability _abilityType = Types.Ability.DAMAGE;
    public Types.Area _areaType = Types.Area.LINEAR;
    public Types.Target _targetType = Types.Target.ENEMY;

    public float _range = 0f;
    public float _recharge = 0f;

    public bool isDone { get; set; }
    public GameObject caster { get; set; }
    public Transform target { get; set; }

    void Start() {
        isDone = false;
    }

    public abstract void DoEffect(GameObject caster, Transform target);

    protected abstract void Effect();
    protected abstract IEnumerator DuraEffect();
    protected abstract void Finish();
}
