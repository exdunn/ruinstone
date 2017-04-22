using UnityEngine;
using System.Collections.Generic;

public abstract class Payload : Behaviour {
    public float _power = 0f;
    public float _duration = 0f;
    public float _area = 0f;

    protected float _internal = 0f;

    void Start() {

    }

    protected List<GameObject> GetAll(Types.Target type) {
        Collider[] t = Physics.OverlapSphere(point.position, _area);
        List<GameObject> all = new List<GameObject>();
        foreach(Collider c in t) {
            if(c.gameObject.CompareTag(Types.TargetToString(type))) {
                all.Add(c.gameObject);
            }
        }
        return all;
    }
}
