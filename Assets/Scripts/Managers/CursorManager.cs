using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardWars
{
    public class CursorManager : MonoBehaviour
    {

        public Texture2D cursorActivated;
        public Texture2D cursorNormal;
        Vector2 mousePos;
        public bool activated
        {
            get; set;
        }

        void Start()
        {
            activated = false;
            Cursor.visible = false;
            DontDestroyOnLoad(this);

            if (FindObjectsOfType(GetType()).Length > 1)
            {
                Destroy(gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {
            mousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        }

        void OnGUI()
        {
            if (activated)
            {
                GUI.DrawTexture(new Rect(mousePos.x - (32 / 2), mousePos.y - (32 / 2), 32, 32), cursorActivated);
            }
            else
            {
                GUI.DrawTexture(new Rect(mousePos.x - (32 / 2), mousePos.y - (32 / 2), 32, 32), cursorNormal);
            }
           
        }
    }
}

