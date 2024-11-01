using Photon.Pun;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // サーバーに接続
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        // ルームに接続できたら自動でルームを作成または参加
        PhotonNetwork.JoinOrCreateRoom("Room1", new Photon.Realtime.RoomOptions(), null); // TypedLobby.Defaultをnullに変更
    }

    public override void OnJoinedRoom()
    {
        // プレイヤーを生成
        PhotonNetwork.Instantiate("PlayerPrefab", new Vector3(0, 0, 0), Quaternion.identity);
    }
}
