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

    public static GameObject CreateProjectile(string prefab, Projectile proj, Transform parent, Vector3 position, Quaternion rotation) {
        // PhotonNetwork API doc-api.photonengine.com/en/pun/current/class_photon_network.html#a843d9f62d28ab123c83291c1e6bb857d
        GameObject spawn = PhotonNetwork.Instantiate(prefab, position, rotation, 0);
        //spawn = CopyProjectile(spawn, proj);
        return spawn;
    }

    public static GameObject CopyProjectile(GameObject spawn, Projectile projectile) {
        Projectile proj = spawn.GetComponent<Projectile>();
        //Debug.Log("Proj is " + proj);
        //Debug.Log("Projectile is " + projectile);
        proj._speed = projectile._speed;
        proj._range = projectile._range;
        proj.Init();
        //proj._rigidbody = spawn.GetComponent<Rigidbody>();
        //proj.transf = spawn.transform.GetChild(0).gameObject;
        //proj.transf.transform.position = new Vector3(0,1.5f,0);
        return spawn;
    }
}
