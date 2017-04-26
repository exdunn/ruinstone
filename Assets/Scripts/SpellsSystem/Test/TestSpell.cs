//using UnityEngine;
//using System;
//using System.Collections;
//using System.Collections.Generic;

//public class TestSpell : Spell {
//    //public Projectile a;
//    //public Displace a;

//    public Projectile _projectile = null;
//    public Behaviour[] _behaviours = null;

//    private bool _begun = false;
//    private GameObject caster;
//    private GameObject target;
//    private Transform point;
//    void Start() {
//        //Set up stats here
//        //Add expected behaviours here
//        //Set up stats here
//        //_behaviours.Enqueue(a);
//    }

//    void FixedUpdate() {
//        Debug.Log(this.transform.position);
//    }
    
//    public override void Activate(GameObject caster, GameObject target, Transform point) {
//        //Basic flow of a spell activation:
//        //Load up the three parameters given
//        //In case of targeted or direct spells, use target here
//        //This target may not be needed (as it will always be null)
//        //For self cast spells, it may just check if it is a self cast without ever checking if the target == caster
//        //For projectile, calculate the direction
//        //Activate the first module (Usually a projectile)
//        //When the projectile is Done, load up target module
//        //This is the target of collision with projectile.
//        //Activate the second - last modules, one by one
//        //Activate the next one only when the previous one is done
//        //Keep going until there are no more modules.
//        //You are done!
//        this.caster = caster;
//        this.target = target;
//        this.point = point;

//        StartCoroutine(DoActivation());
//    }

//    private IEnumerator DoActivation() {
//        //Projectile Stuff
//        Debug.Log(_projectile);
//        if(_projectile != null) {
//            Debug.Log("Moving projectile...");
//            _projectile.DoEffect(caster, target, point);
//            while(!_projectile.isDone) {
//                yield return null;
//            }
//            Debug.Log("Projectile is done moving...");
//            Debug.Log("Collided with ... ");
//            target = _projectile.collidedTarget;
//            point = _projectile.collidedLoc;
//            Debug.Log(target + ", " + point);
//        }

//        //Payload Stuff
//        for(int i = 0; i < _behaviours.Length; ++i) {
//            _behaviours[i].DoEffect(caster, target, point);
//            while(!_behaviours[i].isDone) {
//                yield return null;
//            }
//        }
//    }

//    public override void Finish() {
//        throw new NotImplementedException();
//    }
//}
