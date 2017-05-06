using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardWars
{

    public class PlayerControllerV2 : Photon.MonoBehaviour
    {

        #region public variables 

        #endregion

        #region private variables 

        Vector3 newPosition;

        GameObject[] spells;

        bool isCasting = false;

        [SerializeField]
        GameObject[] spellPrefabs;
        [SerializeField]
        float speed;
        [SerializeField]
        float walkRange;
        [SerializeField]
        float rotationSpeed;
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
                spells[i] = Instantiate(spellPrefabs[0], transform.position, transform.rotation);
                //Debug.Log("Spell: " + spells[i]);
            }
        }

        // Update is called once per frame
        void Update()
        {
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
                isCasting = true;
            }

            // spell targetting state
            bool canSpell = spells[0].GetComponent<Spell>().isCastable;
            //Debug.Log("Castable: " + canSpell);
            if (true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.DrawLine(transform.position, hit.point, Color.red);
                        spells[0].GetComponent<Spell>().Activate(gameObject, null, hit.point);

                        // make player look at target of spell
                        playerModel.transform.LookAt(hit.point);

                        // if player is moving, stop moving
                        newPosition = transform.position;
                    }

                    isCasting = false;
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
                playerModel.transform.rotation = Quaternion.Slerp(lookRotation, playerModel.transform.rotation, rotationSpeed);

                Debug.DrawLine(transform.position, newPosition, Color.red);
            }
        }

        #endregion

    }
}
