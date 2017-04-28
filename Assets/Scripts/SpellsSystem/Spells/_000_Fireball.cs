using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class _000_Fireball : Spell {
    private Projectile _projectile;
    private GameObject _spawn;
    //private int i = 0;
    void Awake() {
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
        _projectile.enabled = false;
        //Debug.Log(i++);
        //Debug.Log("projectile: " + _projectile);
        //Set up Projectile's rigidbody
        //_projectile._rigidbody = _spawnPrefab.GetComponent<Rigidbody>();
    }

    public override void Activate(GameObject caster, GameObject target, Vector3 point) {
        isActive = true;
        //Transform temp = Utils.CreateNewTransform(point);
        StartCoroutine(CoActivate(caster, target, point));
        //Destroy(/*temp*/);
        GoOnCooldown();
    }

    public override void Finish() {
        isDone = true;
    }

    protected override IEnumerator CoActivate(GameObject caster, GameObject target, Vector3 point) {
        //Debug.Log(i++);
        //Debug.Log("proje: " + _projectile);
        //Spawn projectile
        _spawn = Utils.CreateProjectile(_spawnPrefab, _projectile, this.transform, caster.transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
        Projectile proj = _spawn.GetComponent<Projectile>();
        //Move projectile
        //proj.direction = (point - caster.transform.position).normalized;
        //Debug.Log("Activating Projectile");
        //Debug.Log("Caster: " + caster);
        //Debug.Log("Dir: " + point);
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
        isActive = false;
    }
}
