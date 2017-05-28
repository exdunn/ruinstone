using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardWars
{
    public class RuinStoneTheme : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(this);

            if (FindObjectsOfType(GetType()).Length > 1)
            {
                Destroy(gameObject);
            }

            if (PlayerPrefs.GetInt("Theme") == 0) {

                GetComponent<AudioSource>().enabled = false;
            }
            else {

                GetComponent<AudioSource>().enabled = true;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}