using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WizardWars
{
    public class CancelButton : MonoBehaviour
    {
        /// <summary>
        /// Loads Main Menu Scene
        /// </summary>
        public void Cancel()
        {
            SceneManager.LoadScene(0);
        }
    }
}

