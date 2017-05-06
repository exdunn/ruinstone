using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour
{

    Vector3 realPosition = Vector3.zero;
    Quaternion realRotation = Quaternion.identity;
    public GameObject playerModel;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            // Do nothing -- the character motor/input/etc... is moving us
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, realPosition, 0.1f);
            playerModel.transform.rotation = Quaternion.Lerp(playerModel.transform.rotation, realRotation, 0.1f);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // This is OUR player. We need to send our actual position to the network.

            stream.SendNext(transform.position);
            stream.SendNext(playerModel.transform.rotation);
        }
        else
        {
            // This is someone else's player. We need to receive their position (as of a few
            // millisecond ago, and update our version of that player.

            realPosition = (Vector3)stream.ReceiveNext();
            realRotation = (Quaternion)stream.ReceiveNext();
        }

    }
}
