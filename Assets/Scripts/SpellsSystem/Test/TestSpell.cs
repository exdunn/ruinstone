using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TestSpell : Spell {
    //public Projectile a;
    public Displace a;

    public Queue<Behaviour> q = new Queue<Behaviour>();

    private bool _begun = false;
    private GameObject caster;
    private Transform target;
    void Start() {
        //Set up stats here
        //Add expected behaviours here
        //Set up stats here
        q.Enqueue(a);
    }

    void FixedUpdate() {
        Debug.Log(this.transform.position);
    }
    
    public override void Activate(GameObject caster, Transform target) {
        //The Activate method needs to be something like this
        //Behaviours activate one after the other
        //Behaviours need to be activated something like this
        //Behaviour.Effect(Caster, Target)
        //Effect should do everything needed, there should be no outside interaction until it is finished
        //Parameters should be set on inspector before, or can be changed before Effect
        Debug.Log("Activated");
        this.caster = caster;
        this.target = target;
        //a.direction = Vector3.up;
        _begun = true;
        StartCoroutine(DoActivation());
        //b.Effect();
        //c.Effect();
        //d.Effect();
        //e.Effect();
    }

    private IEnumerator DoActivation() {
        while(q.Count > 0) {
            Behaviour b = q.Dequeue();
            b.DoEffect(caster, target);
            while(!b.isDone) {
                yield return null;
            }
        }
    }

    public override void Finish() {
        throw new NotImplementedException();
    }
}
