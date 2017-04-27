using UnityEngine;
using System;
using System.Collections.Generic;

public static class Utils {
	public static Transform CreateNewTransform(Vector3 position) {
        GameObject temp = new GameObject();
        Transform n = temp.transform;
        n.position = position;
        UnityEngine.Object.Destroy(temp);
        return n;
    }

    public static List<GameObject> GetAll(Types.Target type, Vector3 center, float radius) {
        Collider[] t = Physics.OverlapSphere(center, radius);
        List<GameObject> all = new List<GameObject>();
        foreach(Collider c in t) {
            if(c.gameObject.CompareTag(Types.TargetToString(type))) {
                all.Add(c.gameObject);
            }
        }
        return all;
    }

    public static GameObject CreateProjectile(GameObject prefab, Projectile proj, Transform parent, Vector3 position, Quaternion rotation) {
        GameObject spawn = UnityEngine.Object.Instantiate<GameObject>(prefab, position, rotation);
        spawn = CopyProjectile(spawn, proj);
        return spawn;
    }

    public static GameObject CopyProjectile(GameObject spawn, Projectile projectile) {
        spawn.AddComponent<Projectile>();
        Projectile proj = spawn.GetComponent<Projectile>();
        //Debug.Log("Proj is " + proj);
        //Debug.Log("Projectile is " + projectile);
        proj._speed = projectile._speed;
        proj._range = projectile._range;
        proj.Init();
        proj._rigidbody = spawn.GetComponent<Rigidbody>();
        return spawn;
    }
}
