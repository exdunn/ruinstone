using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : Photon.MonoBehaviour {

    Vector3 newPosition;

    [SerializeField]
    float speed;
    [SerializeField]
    float walkRange;
    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    GameObject graphics;

    // Use this for initialization
    void Start ()
    {
        newPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        moveChar();

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
            Quaternion lookRotation = Quaternion.LookRotation(newPosition - transform.position, Vector3.up);
            graphics.transform.rotation = Quaternion.Slerp(lookRotation, graphics.transform.rotation, rotationSpeed);

            Debug.DrawLine(transform.position, newPosition, Color.red);
        }
    }
}
