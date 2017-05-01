using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : Photon.MonoBehaviour {

    Vector3 newPosition;

    GameObject[] spells;
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

    bool isCasting;

    // Use this for initialization
    void Start ()
    {
        isCasting = false;
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
	void Update () {

        moveChar();

        // move player when user presses RMB
        if (Input.GetMouseButtonDown(1))
        {
            // if not the local player
            if (!photonView.isMine)
            {
                return;
            }

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GetComponent<PhotonView>().RPC("ReceivedMove", PhotonTargets.All, hit.point);
            }
        }

        // enter targeting state when user presses spell button
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isCasting = true;
        }

        // spell targetting state
        bool canSpell = spells[0].GetComponent<Spell>().isCastable;
        //Debug.Log("Castable: " + canSpell);
        if (canSpell)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!photonView.isMine)
                {
                    return;
                }

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    spells[0].GetComponent<Spell>().Activate(gameObject, null, hit.point);

                    // make player look at target of spell
                    playerModel.transform.LookAt(hit.point);
                    // if player is moving, stop moving
                    GetComponent<PhotonView>().RPC("ReceivedMove", PhotonTargets.All, transform.position);
                }

                isCasting = false;
            }
        }
    }


    [PunRPC]
    public void ReceivedMove(Vector3 movePos)
    {
        newPosition = movePos;
    }

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
}
