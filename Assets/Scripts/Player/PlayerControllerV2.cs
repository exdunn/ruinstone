﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardWars
{

    public class PlayerControllerV2 : Photon.MonoBehaviour
    {

        #region public variables 

        #endregion

        #region private variables 

        // position of mouse click that player moves to
        public Vector3 newPosition
        {
            get; set;
        }

        GameObject[] spells;

        bool targetting = false;

        // index of spell being targeted
        int curSpell = 0;

        [Tooltip("List of spells the player can use")]
        [SerializeField]
        GameObject[] spellPrefabs;

        [Tooltip("Movement speed of the player")]
        [SerializeField]
        float speed;

        [Tooltip("Minimum range that the player will move to")]
        [SerializeField]
        float walkRange;

        [Tooltip("Rotation speed of the player")]
        [SerializeField]
        float rotationSpeed;

        [Tooltip("Model of the player")]
        [SerializeField]
        GameObject playerModel;

        #endregion

        #region monobehaviour callbacks

        // Use this for initialization
        void Start()
        {
            newPosition = transform.position;

            spells = new GameObject[spellPrefabs.Length];

            for (int i = 0; i < spellPrefabs.Length; ++i)
            {
                //Debug.Log("Loaded Spell");
                spells[i] = Instantiate(spellPrefabs[i], transform.position, transform.rotation);
                //Debug.Log("Spell: " + spells[i]);
            }
        }

        // Update is called once per frame
        void Update()
        {
            // if player is dead then return
            if (GetComponent<PlayerManager>().dead)
            {
                return;
            }

            moveChar();

            // move player when user presses RMB
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    newPosition = hit.point;
                }
            }

            // *********************** CASTING **************************

            // enter targeting state when user presses spell button
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                curSpell = 0;
                targetting = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                curSpell = 1;
                targetting = true;
            }
            else if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                curSpell = 2;
                targetting = true;
            }
            else if(Input.GetKeyDown(KeyCode.Alpha4))
            {
                curSpell = 3;
                spells[curSpell].GetComponent<SpellSystem.Spell>().Cast(gameObject, gameObject, new Vector3(0,0,0));
            }

            // spell targetting state
            //bool canSpell = spells[0].GetComponent<Spell>().isCastable;
            //Debug.Log("Castable: " + canSpell);

            if (targetting)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.DrawLine(transform.position, hit.point, Color.red);
                        spells[curSpell].GetComponent<SpellSystem.Spell>().Cast(gameObject, null, hit.point);

                        // make player look at target of spell
                        playerModel.transform.LookAt(hit.point);

                        // if player is moving, stop moving
                        newPosition = transform.position;
                    }

                    targetting = false;
                }
            }
        }

        #endregion

        #region public methods 

        #endregion

        #region private methods 

        private void moveChar()
        {
            if (Vector3.Distance(newPosition, transform.position) > walkRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

                // update player rotation
                Quaternion lookRotation = Quaternion.LookRotation(newPosition - transform.position, Vector3.up);
                lookRotation = new Quaternion(0, lookRotation.y, 0, lookRotation.w);
                playerModel.transform.rotation = Quaternion.Slerp(lookRotation, playerModel.transform.rotation, rotationSpeed);

                Debug.DrawLine(transform.position, newPosition, Color.red);
            }
        }

        #endregion

    }
}
