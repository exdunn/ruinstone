using UnityEngine;
using System.Collections;


/* Abstract Base Behaviour class */
public abstract class Behaviour : MonoBehaviour {
    protected const float TICK = .1f;

    public Types.Ability _abilityType = Types.Ability.DAMAGE;
    public Types.Area _areaType = Types.Area.LINEAR;
    public Types.Target _targetType = Types.Target.ENEMY;

    public float _range = 0f;
    public float _recharge = 0f;

    public bool isDone { get; set; }
    public int caster { get; set; }
    public Transform target { get; set; }

    void Start() {
        isDone = false;
    }

    public abstract void Effect();
    public abstract void DuraEffect();
    protected abstract IEnumerator Duration();
    protected abstract void Finish();
}
