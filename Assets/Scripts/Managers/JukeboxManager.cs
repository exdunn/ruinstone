using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardWars
{
    public class JukeboxManager : MonoBehaviour
    {
        public AudioClip battleTrack;
		public AudioClip menuTrack;

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

		public void ChangeTrack(int track)
        {
			switch (track) {
			case 0: 
				GetComponent<AudioSource> ().clip = menuTrack;
				break;
			case 1:
				GetComponent<AudioSource>().clip = battleTrack;
				break;
			default:
				break;
			}
        }
    }
}