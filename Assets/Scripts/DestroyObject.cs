using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardWars
{
    public class DestroyObject : MonoBehaviour {

        #region Public Variables

        #endregion

        #region Private Variables

        #endregion

        #region MonoBehaviour Callbacks

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter (Collision other)
        {
            Destroy(gameObject);
        }

        #endregion
    }
}

