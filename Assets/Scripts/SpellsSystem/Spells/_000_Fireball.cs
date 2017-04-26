using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class _000_Fireball : Spell {
    private Projectile _projectile;
    private GameObject _spawn;
    void Start() {
        if(_delivery == null) {
            Debug.Log("There must be a delivery method!");
        }
        else if(_delivery.GetType() != typeof(Projectile)) {
            Debug.Log("Fireball should really have a Projectile delivery!");
        }
        if(_spawnPrefab.GetComponent<Rigidbody>() == null) {
            Debug.Log("The spawned projectile must have a rigidbody");
        }
        _projectile = _delivery as Projectile;
        //Set up Projectile's rigidbody
        //_projectile._rigidbody = _spawnPrefab.GetComponent<Rigidbody>();
    }

    public override void Activate(GameObject caster, GameObject target, Vector3 point) {
        isActive = true;
        GameObject temp = new GameObject();
        temp.transform.position = point;
        Vector3 t = temp.transform.position;
        t.z = 0;
        temp.transform.position = t;
        StartCoroutine(CoActivate(caster, target, temp.transform));
    }

    public override void Finish() {
        isDone = true;
    }

    protected override IEnumerator CoActivate(GameObject caster, GameObject target, Transform point) {
        //Spawn projectile
        _spawn = CreateProjectile(_projectile, caster.transform);
        Projectile proj = _spawn.GetComponent<Projectile>();
        //Move projectile
        proj.DoEffect(caster, target, point);

        //GameObject t; Transform p;
        while(_spawn != null && !proj.isDone) {
            target = proj.collidedTarget;
            point = proj.collidedLoc;
            yield return null;
        }


        //Destroy the projectile
        //Destroy(_spawn);

        //Do Damage
        for(int i = 0; i < _behaviours.Length; ++i) {
            _behaviours[i].DoEffect(caster, target, point);
            while(!_behaviours[i].isDone) {
                yield return null;
            }
        }
    }
}
