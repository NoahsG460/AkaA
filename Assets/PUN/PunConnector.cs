using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Punconnector : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start!");

        PhotonNetwork.ConnectUsingSettings();
    }



    public override void OnConnectedToMaster()
    {
        Debug.Log("Mastar Connected");
        
        Debug.Log("Join Room Start");
        PhotonNetwork.JoinOrCreateRoom("room" , new RoomOptions() , TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Room Joined");
        PhotonNetwork.Instantiate("GameManager", Vector3.zero, Quaternion.identity);    }
}
