using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoverNoPun : MonoBehaviour {

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
    GameObject graphics;

    bool isCasting;
    int playerId;

    // Use this for initialization
    void Start ()
    {
        playerId = PhotonNetwork.player.ID;
        isCasting = false;
        newPosition = transform.position;
        spells = new GameObject[spellPrefabs.Length];

        for (int i = 0; i < spellPrefabs.Length; ++i)
        {
            Debug.Log("Loaded Spell");
            spells[i] = Instantiate(spellPrefabs[0], transform.position, transform.rotation);
            Debug.Log("Spell: " + spells[i]);
        }
    }
	
	// Update is called once per frame
	void Update () {

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

        // enter targeting state when user presses spell button
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isCasting = true;
        }

        // spell targetting state
        bool canSpell = spells[0].GetComponent<Spell>().isCastable;
        Debug.Log("Castable: " + canSpell);
        if (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    //GameObject newSpell = Instantiate(spells[0], transform.position, transform.rotation);
                    spells[0].GetComponent<Spell>().Activate(gameObject, null, hit.point - transform.position);

                }

                isCasting = false;

            }
        }
    }

    private void moveChar()
    {
        if (Vector3.Distance(newPosition, transform.position) > walkRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
            Quaternion lookRotation = Quaternion.LookRotation(newPosition - transform.position, Vector3.up);
            graphics.transform.rotation = Quaternion.Slerp(lookRotation, graphics.transform.rotation, rotationSpeed);

            Debug.DrawLine(transform.position, newPosition, Color.red);
        }
    }
}
