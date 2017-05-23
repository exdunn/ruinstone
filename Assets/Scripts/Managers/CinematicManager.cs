using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WizardWars
{
    public class CinematicManager : MonoBehaviour
    {

        public GameObject[] players;

        // Use this for initialization
        void Start()
        {
            Debug.Log("model: " + players[0].GetComponent<Renderer>().materials[0]);
            players[0].GetComponent<Renderer>().materials[0].color = new Color32(26, 26, 26, 255);
            players[1].GetComponent<Renderer>().materials[0].color = new Color32(124, 15, 15, 255);
            players[2].GetComponent<Renderer>().materials[0].color = new Color32(117, 6, 188, 255);
            players[3].GetComponent<Renderer>().materials[0].color = new Color32(15, 96, 25, 255);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
