using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    [SerializeField]
    float timer;

	// Update is called once per frame
	void FixedUpdate () {
		if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            PhotonNetwork.Destroy(gameObject);
        }

	}
}
