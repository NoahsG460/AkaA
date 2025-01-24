using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PunPlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine) {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 tapPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                tapPosition.z = 0;
                transform.position = tapPosition;
            }
        }
    }
    void IPunObservable.OnPhotonSerializeView(Photon.Pun.PhotonStream stream, Photon.Pun.PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            Vector3 myPosition = transform.position;
            stream.SendNext(myPosition);
        }
        if (stream.IsReading)
        {
            Vector3 otherPosition = (Vector3)stream.ReceiveNext();
            transform.position = otherPosition;
        }
    }
}
