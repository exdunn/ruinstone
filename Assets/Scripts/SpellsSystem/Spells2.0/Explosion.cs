using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public ParticleSystem lightFlare;

    public float timer { get; set; }

    public float size { get; set; }

    void Start()
    {
        ParticleSystem.MainModule main = lightFlare.main;
        main.startSizeMultiplier = size;
    }

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
