using UnityEngine;
using System.Collections;

public class ScreenShot : MonoBehaviour {

    [SerializeField]
    bool ScreenShotActive = false;

    [SerializeField]
    KeyCode ActivateKey = KeyCode.Space;

    [SerializeField]
    string CaptureName = "ScreenShot.png";


	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(ActivateKey) && ScreenShotActive)
        {
            Debug.Log("Screen Shot Captured.");
            Application.CaptureScreenshot(CaptureName);
        }
	}
}
