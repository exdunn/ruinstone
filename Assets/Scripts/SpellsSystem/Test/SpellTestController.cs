using UnityEngine;
using System;
using System.Collections.Generic;

public class SpellTestController : MonoBehaviour {
    public Projectile proj;

    public bool begin = false;
    private bool begun = false;

    void Start() {

    }

    void Update() {
        if(begin && !begun) {
            //proj.origin = this.transform.position;
            proj.direction = Vector3.up;
            proj.Effect();
            begun = true;
        }
    }
}
