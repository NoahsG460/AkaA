using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // �T�[�o�[�ɐڑ�
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("�T�[�o�[�ɐڑ����܂����I");
        PhotonNetwork.JoinLobby(); // ���r�[�ɎQ��
    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    public override void OnJoinedLobby()
    {
        Debug.Log("���r�[�ɎQ�����܂����I");
        PhotonNetwork.JoinOrCreateRoom("Room1", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default); // ���[���쐬�܂��͎Q��
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("���[���ɎQ�����܂����I");
        SceneManager.LoadScene("GameScene"); // �Q�[���V�[�������[�h
    }
}
