using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardWars
{
    public class PlayerController : Photon.MonoBehaviour
    {

        #region Public Variables

        public Spell[] spells;

        #endregion

        #region Private Variables

        /// <summary>
        /// Movement speed scalar
        /// </summary>
        float speed = 0.25f;

        /// <summary>
        /// Rotation speed scalar
        /// </summary>
        float rotationSpeed = 0.67f;

        /// <summary>
        /// Boolean that is true if right mouse button is down, else it is false.
        /// Use for character rotation
        /// </summary>
        bool rightClicked;

        /// <summary>
        /// When true play is in the targetting state
        /// </summary>
        bool casting;

        #endregion

        #region MonoBehaviour CallBacks

        // Use this for initialization
        void Start()
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }

        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            if (photonView.isMine == false && PhotonNetwork.connected == true)
            {
                return;
            }

            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            Rigidbody rb = GetComponent<Rigidbody>();

            Vector3 position = transform.forward * moveZ + transform.right * moveX;
            // movement based on WASD
            rb.MovePosition(rb.transform.position + position * speed);

            if (Input.GetMouseButton(1)) { rightClicked = true; }
            if (Input.GetMouseButtonUp(1)) { rightClicked = false; }

            // player casts spell one
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                casting = true;
            }


            // spell targetting state
            if (casting)
            {
                if (Input.GetMouseButton(1))
                {
                    Debug.Log("CAST SPELL ONE");
                    Debug.DrawRay(transform.position, (Input.mousePosition), Color.green);
                    //spells[0].Activate(gameObject, null, director);
                }
            }
        }

        /// <summary>
        /// LateUpdate is called every frame, if the Behaviour is enabled.
        /// LateUpdate is called after all Update functions have been called.This is useful to order script execution. 
        /// For example a follow camera should always be implemented in LateUpdate because it tracks objects that might have moved inside Update.
        /// </summary>
        void LateUpdate()
        {
            float rotation = Input.GetAxis("Rotation Axis");
            Rigidbody rb = GetComponent<Rigidbody>();

            if (rightClicked && rotation != 0)
            {
                Vector3 rotationY = new Vector3(0f, rotation, 0f);
                rotationY = rotationY * rotationSpeed;
                Quaternion deltaRotation = Quaternion.Euler(rotationY);
                rb.MoveRotation(rb.rotation * deltaRotation);
            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods
        
        /// <summary>
        /// When player casts spell
        /// </summary>
        /// <param name="spell"></param>
        private void CastSpell(int spell)
        {
            Debug.Log("CAST SPELL " + spell);

            // if spell is projectile
            if (true)
            {
                // enter targetting state
                //display targetting reticule

            }
            // if spell is ground
            else if (true)
            {
                // enter targetting state
                // display ground AoE indicator

            }
            // if spell is self
            else if (true)
            {
                // cast the spell
            }
        }

        #endregion

    }
}
