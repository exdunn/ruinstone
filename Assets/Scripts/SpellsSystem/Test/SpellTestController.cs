using UnityEngine;
using System;
using System.Collections.Generic;

public class SpellTestController : MonoBehaviour {
    //public Projectile proj;
    public Spell Cube;
    public GameObject dummy;

    public bool begin = false;
    private bool begun = false;

    void Start() {

    }

    void Update() {
        if(begin && !begun) {
            //proj.origin = this.transform.position;
            Cube.Activate(this.gameObject, dummy, dummy.transform); //Spells need to be activated like this
            //Caster, Target Position
            //Caster cannot be null.
            //Target can be null, which means the target is the caster itself, or it is a linear projectile
            begun = true;
        }
    }
}
