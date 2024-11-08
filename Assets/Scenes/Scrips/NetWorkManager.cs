using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // サーバーに接続
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("サーバーに接続しました！");
        PhotonNetwork.JoinLobby(); // ロビーに参加
    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    public override void OnJoinedLobby()
    {
        Debug.Log("ロビーに参加しました！");
        PhotonNetwork.JoinOrCreateRoom("Room1", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default); // ルーム作成または参加
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("ルームに参加しました！");
        SceneManager.LoadScene("GameScene"); // ゲームシーンをロード
    }
}
